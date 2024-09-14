using System;
using System.Collections.Generic;
using UnityEngine;

public enum HeadStateEnum {
    OnBody,
    Moving,
    Return,
    Charging,
}

public class Head : Agent {
    [Header("Setting Value")]
    private float attackSpeed;
    public float AttackSpeed => attackSpeed;
    private float returnSpeed;
    public float ReturnSpeed => returnSpeed;
    private float attackCooldown;
    public float AttackCooldown => attackCooldown;
    private int att;
    public int Att => att;
    public float NeckLength;
    public LayerMask ReturnLayer;
    [HideInInspector] public float lastAttackTime;
    public HeadStateMachine StateMachine {get; private set;}
    [SerializeField] private InputReader inputReader;
    public Stack<Vector2> ReturnPositionStack = new Stack<Vector2>();
    public InputReader InputReader => inputReader;

    [Header("Charing Data")]
    public PlayerChargingData ChargingData = new PlayerChargingData();

    public Action SparkEvent;

    private CircleCollider2D collider;

    #region Component
    public SpecialAttackExecutor SpecialAttackExecutorCompo {get; private set;}
    #endregion

    private bool extraMove;
    public bool ExtraMove {
        get => extraMove;
        set => extraMove = value;
    }

    [SerializeField] private HeadStateEnum currentStateEnum;
    public HeadStateEnum CurrentStateEnum {
        get => currentStateEnum;
        set => currentStateEnum = value;
    }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new HeadStateMachine();
        foreach (HeadStateEnum stateEnum in Enum.GetValues(typeof(HeadStateEnum))) {
            string typeName = stateEnum.ToString();
            try {
                Type t = Type.GetType($"Head{typeName}State");
                HeadState state = Activator.CreateInstance(t, this, StateMachine, typeName) as HeadState;
                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex) {
                Debug.LogError($"{typeName} is loading error! check Message");
                Debug.LogError(ex.Message);
            }
        }
        collider = GetComponent<CircleCollider2D>();
        SpecialAttackExecutorCompo = GetComponent<SpecialAttackExecutor>();
        ChargingData.Init(this);
        StatInit();
    }

    private void StatInit() {
        PlayerStat stat = this.stat as PlayerStat;
        attackSpeed = stat.shootSpeed.GetValue();
        returnSpeed = stat.returnSpeed.GetValue();
        attackCooldown = stat.attackCooldown.GetValue();
        NeckLength = stat.attackRange.GetValue();
        att = stat.att.GetValue();
    }

    protected void Start() {
        StateMachine.Initialize(HeadStateEnum.OnBody, this);
    }

    protected void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    private Collider2D[] result = new Collider2D[10];
    public Collider2D CollisionCheck() {
        int numColliders = Physics2D.OverlapCircleNonAlloc(transform.position, collider.radius, result, ReturnLayer);
        if (numColliders > 0) {
            for (int i = 0; i < numColliders; ++i) {
                return result[i];
            }
        }
        return null;
    }

    private void OnDrawGizmos() {
        if (collider != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, collider.radius);
            Gizmos.color = Color.white;
        }
    }
}
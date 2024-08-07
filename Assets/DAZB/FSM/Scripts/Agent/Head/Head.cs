using System;
using System.Collections.Generic;
using UnityEngine;

public enum HeadStateEnum {
    OnBody,
    Moving,
    JustMoving,
    Return,
}

public class Head : Agent {
    [Header("Setting Value")]
    public float AttackSpeed;
    public float ReturnSpeed;
    public float NeckLength;
    public float AttackCooldown;
    [HideInInspector] public float lastAttackTime;
    public HeadStateMachine StateMachine {get; private set;}
    [SerializeField] private InputReader inputReader;
    public Stack<Vector2> ReturnPositionStack = new Stack<Vector2>();
    public InputReader InputReader => inputReader;
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
    }

    protected void Start() {
        StateMachine.Initialize(HeadStateEnum.OnBody, this);
    }

    protected void Update() {
        StateMachine.CurrentState.UpdateState();
    }
}
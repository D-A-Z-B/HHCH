using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyStateEnum {
    Idle,
    Walk,
    Jump,
    Fall
}

public class Body : Agent
{
    [Header("Setting Value")]
    private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    public float jumpPower;
    
    public BodyStateMachine StateMachine {get; protected set;}
    [SerializeField] private InputReader inputReader;
    public InputReader InputReader => inputReader;

    

    [SerializeField] private BodyStateEnum currentStateEnum;
    public BodyStateEnum CurrentStateEnum {
        get => currentStateEnum;
        set => currentStateEnum = value;
    }

    protected override void Awake() {
        base.Awake();
        StateMachine = new BodyStateMachine();
        foreach (BodyStateEnum stateEnum in Enum.GetValues(typeof(BodyStateEnum))) {
            string typeName = stateEnum.ToString();
            try {
                Type t = Type.GetType($"Body{typeName}State");
                BodyState state = Activator.CreateInstance(t, this, StateMachine, typeName) as BodyState;
                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex) {
                Debug.LogError($"{typeName} is loading error! check Message");
                Debug.LogError(ex.Message);
            }
        }
        StatInit();
    }

    private void StatInit() {
        PlayerStat stat = this.stat as PlayerStat;
        moveSpeed = stat.moveSpeed.GetValue();
    }

    protected void Start() {
        StateMachine.Initialize(BodyStateEnum.Idle, this);
    }

    protected void Update() {
        StateMachine.CurrentState.UpdateState();
    }
}

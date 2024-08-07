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
    [Header("Setting Values")]
    public float moveSpeed;
    public float jumpPower;
    public BodyStateMachine StateMachine {get; protected set;}
    [SerializeField] private InputReader inputReader;
    public InputReader InputReader => inputReader;

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
    }

    protected void Start() {
        StateMachine.Initialize(BodyStateEnum.Idle, this);
    }

    protected void Update() {
        StateMachine.CurrentState.UpdateState();
    }
}

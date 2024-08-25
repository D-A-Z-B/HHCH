using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyStateMachine
{
    public BodyState CurrentState {get; private set;}
    public Dictionary<BodyStateEnum, BodyState> stateDictionary;
    private Body body;

    public BodyStateMachine() {
        stateDictionary = new Dictionary<BodyStateEnum, BodyState>();
    }

    public void Initialize(BodyStateEnum startState, Body body) {
        this.body = body;
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(BodyStateEnum newState) {
        if (body.CanStateChangeable == false) return;
        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(BodyStateEnum stateEnum, BodyState playerState) {
        stateDictionary.Add(stateEnum, playerState);
    }
}

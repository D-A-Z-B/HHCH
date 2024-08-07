using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HeadStateMachine {
    public HeadState CurrentState {get; private set;}
    public Dictionary<HeadStateEnum, HeadState> stateDictionary;
    private Head head;

    public HeadStateMachine() {
        stateDictionary = new Dictionary<HeadStateEnum, HeadState>();
    }

    public void Initialize(HeadStateEnum startState, Head head) {
        this.head = head;
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(HeadStateEnum newState) {
        if (head.CanStateChangeable == false) return;
        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(HeadStateEnum stateEnum, HeadState headState) {
        stateDictionary.Add(stateEnum, headState);
    }
}
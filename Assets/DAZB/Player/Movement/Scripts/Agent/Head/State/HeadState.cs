using System;
using UnityEngine;

public class HeadState {
    protected HeadStateMachine stateMachine;
    protected Head head;
    protected int animBoolHash;
    protected bool endTriggerCalled;
    public HeadStateEnum StateEnum;

    public HeadState(Head head, HeadStateMachine stateMachine, string animBoolName) {
        this.head = head;
        this.stateMachine = stateMachine;
        animBoolHash = Animator.StringToHash(animBoolName);
        if (Enum.TryParse(animBoolName, out HeadStateEnum stateEnum)) {
            StateEnum = stateEnum;
        }
    }

    public virtual void Enter() {
        if (head.AnimatorCompo != null) {
            head.AnimatorCompo.SetBool(animBoolHash, true);
        }
        else {
            Debug.LogWarning("Animator Component does not exist");
        }
        head.CurrentStateEnum = StateEnum;
        endTriggerCalled = false;
    }

    public virtual void UpdateState() { }

    public virtual void Exit() {
        if (head.AnimatorCompo != null) {
            head.AnimatorCompo.SetBool(animBoolHash, false);
        }
        else {
            Debug.LogWarning("Animator Component does not exist");
        }
    }

    public virtual void AnimationFinishTrigger() {
        endTriggerCalled = true;
    }
}
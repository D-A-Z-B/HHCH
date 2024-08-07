using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyState
{
    protected BodyStateMachine stateMachine;
    protected Body body;
    protected int animBoolHash;
    protected bool endTriggerCalled;

    public BodyState(Body body, BodyStateMachine stateMachine, string animBoolName) {
        this.body = body;
        this.stateMachine = stateMachine;
        animBoolHash = Animator.StringToHash(animBoolName);
    }

    public virtual void Enter() {
        if (body.AnimatorCompo != null) {
            body.AnimatorCompo.SetBool(animBoolHash, true);
        }
        else {
            Debug.LogWarning("Animator Component does not exist.");
        }
        endTriggerCalled = false;
    }

    public virtual void UpdateState() {
        
    }

    public virtual void Exit() {
        if (body.AnimatorCompo != null)
            body.AnimatorCompo.SetBool(animBoolHash, false);
        else {
            Debug.LogWarning("Animator Component does not exist.");
        }
    }

    public virtual void AnimationFinishTrigger() {
        endTriggerCalled = true;
    }
}

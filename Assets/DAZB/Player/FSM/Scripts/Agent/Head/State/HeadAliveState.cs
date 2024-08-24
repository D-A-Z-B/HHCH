using UnityEngine;

public class HeadAliveState : HeadState
{
    public HeadAliveState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        
    }

    public override void Exit() {
        base.Exit();
    }
}
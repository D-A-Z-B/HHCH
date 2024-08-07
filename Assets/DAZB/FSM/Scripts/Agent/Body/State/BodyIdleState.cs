using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyIdleState : BodyState
{
    public BodyIdleState(Body body, BodyStateMachine stateMachine, string boolName) : base(body, stateMachine, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        body.MovementCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (body.InputReader.Movement.sqrMagnitude > 0.01f) {
            stateMachine.ChangeState(BodyStateEnum.Walk);
        }
    }
}

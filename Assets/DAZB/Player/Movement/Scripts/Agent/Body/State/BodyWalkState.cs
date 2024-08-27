using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyWalkState : BodyGroundState
{
    public BodyWalkState(Body body, BodyStateMachine stateMachine, string animBoolName) : base(body, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        HandleMovementEvent();
    }


    private void HandleMovementEvent() {
        if (body.InputReader.Movement.sqrMagnitude < Mathf.Epsilon) {
            body.StateMachine.ChangeState(BodyStateEnum.Idle);
        }
        else {
            (body.MovementCompo as AgentMovement).SetMovement(body.InputReader.Movement * body.MoveSpeed);
        }
    }
}

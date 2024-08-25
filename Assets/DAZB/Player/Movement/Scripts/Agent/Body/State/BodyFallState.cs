using UnityEngine;

public class BodyFallState : BodyState
{
    public BodyFallState(Body body, BodyStateMachine stateMachine, string animBoolName) : base(body, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if ((body.MovementCompo as AgentMovement).IsGround) {
            stateMachine.ChangeState(BodyStateEnum.Idle);
        }
        Move();
    }

    private void Move() {
        (body.MovementCompo as AgentMovement).SetMovement(body.InputReader.Movement * body.moveSpeed);
    }
}
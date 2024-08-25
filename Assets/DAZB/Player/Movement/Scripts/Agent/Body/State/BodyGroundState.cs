using UnityEngine;

public class BodyGroundState : BodyState
{
    public BodyGroundState(Body body, BodyStateMachine stateMachine, string animBoolName) : base(body, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        body.InputReader.JumpEvent += HandleJumpEvent;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (body.RigidCompo.velocity.y < 0) {
            stateMachine.ChangeState(BodyStateEnum.Fall);
        }
    }

    public override void Exit()
    {
        body.InputReader.JumpEvent -= HandleJumpEvent;
        base.Exit();
    }

    private void HandleJumpEvent() {
        if (!(body.MovementCompo as AgentMovement).IsGround) return;
        stateMachine.ChangeState(BodyStateEnum.Jump);
    }
}
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
            
        }
    }

    public override void Exit()
    {
        body.InputReader.JumpEvent += HandleJumpEvent;
        base.Exit();
    }

    private void HandleJumpEvent() {
        stateMachine.ChangeState(BodyStateEnum.Jump);
    }
}
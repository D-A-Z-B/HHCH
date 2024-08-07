using UnityEngine;

public class BodyJumpState : BodyState
{
    public BodyJumpState(Body body, BodyStateMachine stateMachine, string animBoolName) : base(body, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    private void Jump() {
        body.RigidCompo.AddForce(Vector2.up *  body.jumpPower, ForceMode2D.Impulse);
    }
}
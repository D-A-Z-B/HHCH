using UnityEngine;

public class HeadOnBodyState : HeadAliveState
{
    public HeadOnBodyState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        head.InputReader.MovingEvent += HandleAttackEvent;
        head.transform.eulerAngles = Vector2.zero;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        head.transform.position = PlayerManager.Instance.Body.transform.position + new Vector3(0, 1, 0);
    }

    public override void Exit()
    {
        head.InputReader.MovingEvent -= HandleAttackEvent;
        base.Exit();
    }

    private void HandleAttackEvent() {
        if (!head.ExtraMove) {
            if (head.lastAttackTime + head.AttackCooldown <= Time.time) {
                stateMachine.ChangeState(HeadStateEnum.Moving);
            }
        }
    }
}
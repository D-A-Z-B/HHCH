using UnityEngine;

public class HeadOnBodyState : HeadState
{
    public HeadOnBodyState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        head.InputReader.AttackEvent += HandleAttackEvent;
        head.transform.eulerAngles = Vector2.zero;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        head.transform.position = PlayerManager.Instance.Body.transform.position + new Vector3(0, 1, 0);
    }

    public override void Exit()
    {
        head.InputReader.AttackEvent -= HandleAttackEvent;
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
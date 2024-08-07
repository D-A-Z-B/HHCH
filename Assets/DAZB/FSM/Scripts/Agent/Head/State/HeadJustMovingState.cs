using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class HeadJustMovingState : HeadState
{
    public HeadJustMovingState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    Coroutine coroutine;
    public override void Enter()
    {
        base.Enter();

        head.InputReader.AttackEvent += HandleAttackEvent;

        coroutine = head.StartCoroutine(Routine());
    }

    public override void Exit()
    {
        head.StopCoroutine(coroutine);
        head.InputReader.AttackEvent -= HandleAttackEvent;
        base.Exit();
    }

    private IEnumerator Routine() {
        float elapsedTime = 0;
        float duration = 3;

        Time.timeScale = 0.1f;
        while (duration > elapsedTime) {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private void HandleAttackEvent() {
        head.ExtraMove = true;
        Time.timeScale = 1f;
        stateMachine.ChangeState(HeadStateEnum.Moving);
    }
}
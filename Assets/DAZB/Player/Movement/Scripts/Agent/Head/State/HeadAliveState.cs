using System;
using UnityEngine;

public class HeadAliveState : HeadState
{
    public HeadAliveState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        head.InputReader.SpecialAttackEvent += HandleSpecialAttackEvent;
    }

    public override void Exit() {
        head.InputReader.SpecialAttackEvent -= HandleSpecialAttackEvent;
        base.Exit();
    }

    private void HandleSpecialAttackEvent() {
        head.SpecialAttackExecutorCompo.Execute();
    }
}
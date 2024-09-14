using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadChargingState : HeadState
{
    private EvolutionCharging charingSO;
    private PlayerStat stat;
    public HeadChargingState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
        stat = head.stat as PlayerStat;
    }

    public override void Enter()
    {
        base.Enter();
        if (EvolutionManager.Instance.IsAppliedEvolution("Charging", out EvolutionEffectSO so)) {
            charingSO = so as EvolutionCharging;
        }
        head.StartCoroutine(ChargingRoutine());
    }

    public override void Exit()
    {
        head.ChargingData.isFinish = true;
        base.Exit();
    }

    private IEnumerator ChargingRoutine() {
        float maxCharingTime = 6;
        float elapsedTime = 0;
        float angle = 0;
        while (true) {
            if (!Mouse.current.leftButton.isPressed) {
                stateMachine.ChangeState(HeadStateEnum.Moving);
                yield break;
            }
            if (elapsedTime >= maxCharingTime) {
                stateMachine.ChangeState(HeadStateEnum.Moving);
                yield break;
            }

            head.ChargingData.currentAtt = Mathf.Lerp(stat.att.GetValue(), charingSO.maxAtt, elapsedTime / maxCharingTime);
            head.ChargingData.currentAttackRange = Mathf.Lerp(stat.attackRange.GetValue(), charingSO.maxAttackRange, elapsedTime / maxCharingTime);
            head.ChargingData.currentAttackSpeed = Mathf.Lerp(stat.shootSpeed.GetValue(), charingSO.maxAttackSpeed, elapsedTime / maxCharingTime);
            head.ChargingData.currentReturnSpeed = Mathf.Lerp(stat.returnSpeed.GetValue(), charingSO.maxReturnSpeed, elapsedTime / maxCharingTime);
            head.ChargingData.currentNeckRotSpeed = Mathf.Lerp(charingSO.maxNeckRotSpeed / 4, charingSO.maxNeckRotSpeed, elapsedTime / maxCharingTime);
            if (angle < 360) {
                angle += Time.deltaTime * head.ChargingData.currentNeckRotSpeed;
                head.transform.position = PlayerManager.Instance.Body.transform.position + new Vector3(0, 0.5f) + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 1f, Mathf.Sin(angle * Mathf.Deg2Rad) * 1f);
            }
            else {
                angle = 0;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
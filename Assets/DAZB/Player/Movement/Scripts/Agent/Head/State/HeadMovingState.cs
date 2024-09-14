using System.Collections;
using UnityEngine;

public class HeadMovingState : HeadAliveState
{
    public HeadMovingState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }
    
    Vector2 startPos;
    Vector2 mousePos;

    public override void Enter()
    {
        base.Enter();
        if (EvolutionManager.Instance.IsAppliedEvolution("Charging") && head.ChargingData.isFinish == false) {
            stateMachine.ChangeState(HeadStateEnum.Charging);
            return;
        }
        head.ReturnPositionStack.Push(head.transform.position);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPos = head.transform.position;
        head.StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine() {
        float elapsedTime = 0f;
        float duration = 1f / head.ChargingData.currentAttackSpeed;
        Vector2 targetPos = mousePos;

        if (Vector2.Distance(startPos, mousePos) > head.NeckLength) {
            targetPos = startPos + (mousePos - startPos).normalized * head.ChargingData.currentAttackRange;
        }

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            float easedT = EaseOutQuart(t);
            head.transform.position = Vector2.Lerp(startPos, targetPos, easedT);
            Vector2 moveDir = (targetPos - (Vector2)head.transform.position).normalized;
            head.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90);
            elapsedTime += Time.deltaTime;

            if (Vector2.Distance(head.transform.position, targetPos) <= 0.1f) {
                stateMachine.ChangeState(HeadStateEnum.Return);
                yield break;
            }

            Collider2D checkCollider = head.CollisionCheck();
            if (checkCollider != null) {
                if (AbilityManager.Instance.IsAppliedAbility(AbilityType.Spark) && checkCollider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                    head.SparkEvent?.Invoke();
                }

                if (checkCollider.TryGetComponent<FieldItem>(out FieldItem item)) {
                    item.Interact();
                }
                stateMachine.ChangeState(HeadStateEnum.Return);
                yield break;
            }

            yield return null;
        }
    }

    public override void Exit()
    {
        head.lastAttackTime = Time.time;
        head.ChargingData.SetDefaultStat();
        head.ChargingData.SetFinish(false);
        head.ExtraMove = false;
        base.Exit();
    }

    private float EaseOutQuart(float t) {
        return 1 - Mathf.Pow(1 - t, 4);
    }
}

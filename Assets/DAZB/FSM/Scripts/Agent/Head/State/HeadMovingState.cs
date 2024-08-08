using System.Collections;
using UnityEngine;

public class HeadMovingState : HeadState
{
    public HeadMovingState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }
    Vector2 startPos;
    Vector2 mousePos;

    public override void Enter()
    {
        base.Enter();
        head.ReturnPositionStack.Push(head.transform.position);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPos = head.transform.position;
        head.StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine() {
        float elapsedTime = 0f;
        float duration = 1f / head.AttackSpeed;
        Vector2 targetPos = mousePos;

        if (Vector2.Distance(startPos, mousePos) > head.NeckLength) {
            targetPos = startPos + (mousePos - startPos).normalized * head.NeckLength;
        }

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            float easedT = EaseOutQuart(t);
            head.transform.position = Vector2.Lerp(startPos, targetPos, easedT);
            Vector2 moveDir = (targetPos - (Vector2)head.transform.position).normalized;
            head.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90);
            elapsedTime += Time.deltaTime;

            if (Vector2.Distance(head.transform.position, targetPos) <= 0.1f) {

                if (JustMovingCheck() && !head.ExtraMove) {
                    stateMachine.ChangeState(HeadStateEnum.JustMoving);
                    yield break;
                }
                stateMachine.ChangeState(HeadStateEnum.Return);
                yield break;
            }

            Collider2D checkCollider = head.CollisionCheck();
            if (checkCollider != null) {
                
            }

            yield return null;
        }
    }

    public override void Exit()
    {
        base.Exit();
        head.lastAttackTime = Time.time;
        head.ExtraMove = false;
    }

    // 아직 구현되지 않은 함수
    private bool JustMovingCheck() {
        return false;
    }

    private float EaseOutQuart(float t) {
        return 1 - Mathf.Pow(1 - t, 4);
    }
}

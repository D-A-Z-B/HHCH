using System.Collections;
using UnityEngine;

public class HeadReturnState : HeadState
{
    public HeadReturnState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        head.StartCoroutine(ReturnRoutine());
    }

    private IEnumerator ReturnRoutine() {
        while (head.ReturnPositionStack.Count > 0) {
            Vector2 startPos = head.transform.position;
            Vector2 endPos = head.ReturnPositionStack.Pop();
            
            float elapsedTime = 0;
            float duration = 1 / head.ReturnSpeed;

            while (Vector2.Distance(head.transform.position, endPos) > 0.1f) {
                elapsedTime += Time.deltaTime;
                if (head.ReturnPositionStack.Count == 0) {
                    endPos = PlayerManager.Instance.Body.transform.position + new Vector3(0, 1, 0);
                }
                head.transform.position = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
                yield return null;
            }
            head.MovementCompo.StopImmediately();
            yield return null;
        }
        head.ReturnPositionStack.Clear();
        stateMachine.ChangeState(HeadStateEnum.OnBody);
    }
}
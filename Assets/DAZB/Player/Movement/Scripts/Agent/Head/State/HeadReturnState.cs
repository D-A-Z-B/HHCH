using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadReturnState : HeadAliveState
{
    public HeadReturnState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (AbilityManager.Instance.IsAppliedAbility(AbilityType.Reignite) && AbilityManager.Instance.IsAppliedAbility(AbilityType.ComeBack)) {
            head.StartDelayCallback(() => Mouse.current.leftButton.wasPressedThisFrame, () => stateMachine.ChangeState(HeadStateEnum.OnBody));
            return;
        }

        if (AbilityManager.Instance.IsAppliedAbility(AbilityType.Reignite)) {
            head.StartDelayCallback(0.3f, () => stateMachine.ChangeState(HeadStateEnum.OnBody));
            return;
        }

        if (AbilityManager.Instance.IsAppliedAbility(AbilityType.ComeBack)) {
            head.StartDelayCallback(() => Mouse.current.leftButton.wasPressedThisFrame, () => head.StartCoroutine(ReturnRoutine()));
            return;        
        }

        head.StartDelayCallback(0.3f, () => head.StartCoroutine(ReturnRoutine()));
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
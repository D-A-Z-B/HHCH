using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Agent : MonoBehaviour
{
    public IMovement MovementCompo {get; private set;}
    public Animator AnimatorCompo {get; private set;}
    public Rigidbody2D RigidCompo {get; private set;}
    public bool CanStateChangeable {get; private set;} = true;

    protected virtual void Awake() {
        MovementCompo = GetComponent<IMovement>();
        AnimatorCompo = GetComponent<Animator>();
        RigidCompo = GetComponent<Rigidbody2D>();
        RigidCompo.interpolation = RigidbodyInterpolation2D.Interpolate;
        MovementCompo.Initialize(this);
    }

    #region Delay callback coroutine
    public Coroutine StartDelayCallback(float delayTime, Action Callback)
    {
        return StartCoroutine(DelayCoroutine(delayTime, Callback));
    }

    public Coroutine StartDelayCallback(Func<bool> delayAction, Action Callback)
    {
        return StartCoroutine(DelayCoroutine(delayAction, Callback));
    }


    protected IEnumerator DelayCoroutine(float delayTime, Action Callback)
    {
        yield return new WaitForSeconds(delayTime);
        Callback?.Invoke();
    }

    protected IEnumerator DelayCoroutine(Func<bool> delayAction, Action callback)
    {
        yield return new WaitUntil(() => delayAction.Invoke());
        callback?.Invoke();
    }
    #endregion
}

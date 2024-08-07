using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

public enum ActionMapType {
    Body, Head,
}

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IBodyActions, IHeadActions
{

#region Body 

    public Action JumpEvent;

#endregion

#region Head

    public Action AttackEvent;

#endregion
    
    private Controls _controls;
    public Controls GetControl()
    {
        return _controls;
    }

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Body.SetCallbacks(this);
            _controls.Head.SetCallbacks(this);
        }

        _controls.Body.Enable();
        _controls.Head.Enable();
    }

    public void Active(ActionMapType type, bool value) {
        switch (type) {
            case ActionMapType.Body: {
                if (value) {
                    _controls.Body.Enable();
                }
                else {
                    _controls.Body.Disable();
                }
                break;
            }
            case ActionMapType.Head: {
                if (value) {
                    _controls.Head.Enable();
                }
                else {
                    _controls.Head.Disable();
                }
                break;
            }
        }
    }

#region BodyEvent

    public Vector2 Movement;
    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

#endregion

#region HeadEvent

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) {
            AttackEvent?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context. performed) {
            JumpEvent?.Invoke();
        }
    }

#endregion
}

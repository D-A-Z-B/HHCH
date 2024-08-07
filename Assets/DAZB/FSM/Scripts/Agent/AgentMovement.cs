using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour, IMovement
{
    [Header("Ground Check")]
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask groundLayer;
    private Body agent;

    private Vector2 velocity;
    public Vector2 Velocity => velocity;
    public bool IsGround => IsGroundMethod();

    public void Initialize(Agent agent) {
        this.agent = agent as Body;
    }

    private void FixedUpdate() {
        Move();
        Flip();
    }

    public void SetMovement(Vector3 movement) {
        velocity = movement;
    }

    private void Flip() {
        if (agent.InputReader.Movement.x > 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (agent.InputReader.Movement.x < 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void StopImmediately() {
        velocity = Vector2.zero;
    }

    private void Move() {
        agent.RigidCompo.velocity = new Vector3(velocity.x, agent.RigidCompo.velocity.y);
    }

    private bool IsGroundMethod() {
        if (Physics2D.OverlapBox(transform.position + offset, groundCheckSize, 0, groundLayer)) {
            return true;
        }
        else {
            return false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, groundCheckSize);
    }
}

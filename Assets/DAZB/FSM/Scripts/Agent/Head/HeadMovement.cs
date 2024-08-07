using UnityEngine;

public class HeadMovement : MonoBehaviour, IMovement
{
    private Head agent;

    public void Initialize(Agent agent) {
        this.agent = agent as Head;
    }

    public void StopImmediately() {
        agent.RigidCompo.velocity = Vector2.zero;
    }
}
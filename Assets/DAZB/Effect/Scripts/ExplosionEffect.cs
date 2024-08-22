using UnityEngine;

public class ExplosionEffect : Effect
{
    public override void AnimationEnd() {
        Destroy(gameObject);
    }
}
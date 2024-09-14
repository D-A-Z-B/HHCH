using UnityEngine;

public abstract class EvolutionEffectSO : ScriptableObject {
    public bool AlreadyApplied = false;

    public abstract void ApplyEffect();
    public virtual void RemoveEffect() {
        AlreadyApplied = false;
    }
}
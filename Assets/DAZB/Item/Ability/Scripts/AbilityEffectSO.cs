using UnityEngine;
public abstract class AbilityEffectSO : ScriptableObject {
    public bool AlreadyApplied = false;
    public AbilityType type;
    public virtual void RemoveEffect() {
        AlreadyApplied = false;
    }
    public abstract void ApplyEffect();
}
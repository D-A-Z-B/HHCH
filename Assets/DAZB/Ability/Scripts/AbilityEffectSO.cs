using UnityEngine;
public abstract class AbilityEffectSO : ScriptableObject {
    public bool AlreadyApplied = false;
    public AbilityType type;
    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}
using UnityEngine;

public abstract class SpecialAttackSO :ScriptableObject {
    public SpecialAttackType type;
    public float Cooldown;
    protected float lastAttackTime;

    public abstract bool CanUseSpecialAttack();
    public abstract void UseSpecialAttack();
}
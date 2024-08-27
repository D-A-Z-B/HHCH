using UnityEngine;

public abstract class SpecialAttackSO :ScriptableObject {
    public SpecialAttackType type;
    public float Cooldown;
    protected float lastAttackTime;
    protected bool isFirst = true;

    public abstract bool CanUseSpecialAttack();
    public abstract void UseSpecialAttack();
}
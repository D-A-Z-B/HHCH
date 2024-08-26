using UnityEngine;

public class AbilityApShot : AbilityEffectSO
{
    private void OnValidate() {
        type = AbilityType.ApShot;

    }

    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            AbilityManager.Instance.ApplyAbility(type);
        }
    }

    public override void RemoveEffect() {

    }
}
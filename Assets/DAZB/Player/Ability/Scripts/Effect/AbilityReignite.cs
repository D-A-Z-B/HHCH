
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/Reignite")]
public class AbilityReignite : AbilityEffectSO
{
    private void OnValidate() {
        type = AbilityType.Reignite;
    }

    public override void ApplyEffect()
    {
        if (AlreadyApplied == false) {
            AlreadyApplied = true;
            AbilityManager.Instance.ApplyAbility(type);
        }
    }

    public override void RemoveEffect() {

    }
}
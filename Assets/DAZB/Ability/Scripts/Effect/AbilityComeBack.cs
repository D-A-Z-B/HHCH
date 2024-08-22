using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/ComeBack")]
public class AbilityComeBack : AbilityEffectSO
{
    private void OnValidate() {
        type = AbilityType.ComeBack;
    }

    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AbilityManager.Instance.ApplyAbility(type);
        }
    }
    public override void RemoveEffect() {

    }
}
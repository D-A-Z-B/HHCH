using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/ComeBack")]
public class AbilityComeBack : AbilityEffectSO
{
    private bool isAppliedThisTime;
    public bool IsAppliedThisTime {
        get => isAppliedThisTime;
        set {
            isAppliedThisTime = value;
        }
    }

    private void OnValidate() {
        type = AbilityType.ComeBack;
    }

    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            isAppliedThisTime = true;
            AbilityManager.Instance.ApplyAbility(type);
        }
    }
    
}
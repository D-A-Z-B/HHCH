using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Ability")]
public class AbilityItem : Item
{
    public AbilityEffectSO effectSO;

    public override void ItemApply() {
        AbilityManager.Instance.ApplyAbility(effectSO.type);
    }

    public override void TempItemApply() {
        AbilityManager.Instance.RemoveTempAbility();
        AbilityManager.Instance.ApplyAbility(effectSO.type, true);
    }
}
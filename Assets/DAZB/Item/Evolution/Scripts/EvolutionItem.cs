using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Evolution")]
public class EvolutionItem : Item
{
    public string evolutionName;
    public EvolutionEffectSO effectSO;
    public List<EvolutionItem> nextEvolutionItemList;

    public override void ItemApply() {
        EvolutionManager.Instance.ApplyEvolution(this);
    }

    public override void TempItemApply() {
        EvolutionManager.Instance.RemoveTempEvolution();
        EvolutionManager.Instance.ApplyEvolution(this, true);
        effectSO.ApplyEffect();
    }
}
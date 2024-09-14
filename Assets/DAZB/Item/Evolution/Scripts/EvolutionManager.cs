using System.Collections.Generic;
using UnityEngine;

public class EvolutionManager : MonoSingleton<EvolutionManager> {
    public List<EvolutionItem> firstEvolutionChoice;
    [SerializeField] private List<EvolutionItem> appliedEvolutionList;
    private EvolutionItem currentEvolution;
    private EvolutionItem tempEvolution;

    public void ApplyEvolution(EvolutionItem item, bool isTemp = false) {
        if (isTemp) {
            tempEvolution = item;
        }
        else {
            currentEvolution = item;
        }
        item.effectSO.ApplyEffect();
        appliedEvolutionList.Add(item);
    }

    public void RemoveEvolution(string evolutionName) {
        foreach (EvolutionItem item in appliedEvolutionList) {
            if (item.evolutionName == evolutionName) {
                foreach (EvolutionItem childItem in item.nextEvolutionItemList) {
                    RemoveEvolution(childItem.evolutionName);
                }
                appliedEvolutionList.Remove(item);
                return;
            }
        }
    }

    public void RemoveTempEvolution() {
        if (tempEvolution == null) return;
        foreach (EvolutionItem iter in appliedEvolutionList) {
            if (iter.evolutionName == tempEvolution.evolutionName) {
                tempEvolution = null;
                appliedEvolutionList.Remove(iter);
                iter.effectSO.RemoveEffect();
                return;
            }
        }
    }

    public List<EvolutionItem> GetNextEvolutionItemList() {
        return currentEvolution.nextEvolutionItemList;
    }

    public bool IsAppliedEvolution(string evolutionName) {
        foreach (EvolutionItem item in appliedEvolutionList) {
            if (item.evolutionName == evolutionName) {
                return true;
            }
        }
        return false;
    }

    public bool IsAppliedEvolution(string evolutionName, out EvolutionEffectSO so) {
        foreach (EvolutionItem item in appliedEvolutionList) {
            if (item.evolutionName == evolutionName) {
                so = item.effectSO;
                return true;
            }
        }
        so = null;
        return false;
    }
}
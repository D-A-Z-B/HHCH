using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoSingleton<AbilityManager> {
    public List<AbilityEffectSO> AbilityEffectSOList;
    private Dictionary<AbilityType, bool> AppliedAbilityDictionary = new Dictionary<AbilityType, bool>();
    private Dictionary<AbilityType, AbilityEffectSO> AbilityDictionary = new Dictionary<AbilityType, AbilityEffectSO>();

    private void Awake() {
        foreach (AbilityEffectSO effect in AbilityEffectSOList) {
            AppliedAbilityDictionary[effect.type] = false;
            AbilityDictionary[effect.type] = Instantiate(effect);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            ApplyAbility(AbilityType.Spark);
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            ApplyAbility(AbilityType.ComeBack);
        }
    }

    public void ApplyAbility(AbilityType type) {
        AppliedAbilityDictionary[type] = true;
        AbilityDictionary[type].ApplyEffect();
    }

    public void RemoveAbility(AbilityType type) {
        AppliedAbilityDictionary[type] = false;
        AbilityDictionary[type].RemoveEffect();
    }

    public bool IsAppliedAbility(AbilityType type) {
        return AppliedAbilityDictionary[type];
    }
}
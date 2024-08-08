using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AbilityManager : MonoSingleton<AbilityManager> {
    public List<AbilityEffectSO> AbilityEffectSOList;
    private Dictionary<AbilityType, bool> AppliedAbilityDictionary = new Dictionary<AbilityType, bool>();

    private void Awake() {
        foreach (AbilityEffectSO effect in AbilityEffectSOList) {
            AppliedAbilityDictionary[effect.type] = false;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            ApplyAbility(AbilityType.Reignite);
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            ApplyAbility(AbilityType.ComeBack);
        }
    }

    public void ApplyAbility(AbilityType type) {
        AppliedAbilityDictionary[type] = true;
    }

    public void RemoveAbility(AbilityType type) {
        AppliedAbilityDictionary[type] = false;
    }

    public bool GetAppliedAbility(AbilityType type) {
        return AppliedAbilityDictionary[type];
    }
}
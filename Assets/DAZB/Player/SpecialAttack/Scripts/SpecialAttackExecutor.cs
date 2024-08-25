using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackExecutor : MonoBehaviour {
    public List<SpecialAttackSO> SpecialAttackSOList;
    private SpecialAttackSO currentSpecialAttack;

    private Dictionary<SpecialAttackType, SpecialAttackSO> specialAttackSODictionary;


    private void Awake() {
        SpecialAttackSODictionaryInitialize();

        // 테스트
        SelectSpecialAttack(SpecialAttackType.FlameShoot);
    }

    public void SelectSpecialAttack(SpecialAttackType type) {
        currentSpecialAttack = specialAttackSODictionary[type];
    }

    public void Execute() {
        if (currentSpecialAttack.CanUseSpecialAttack()) {
            currentSpecialAttack.UseSpecialAttack();
        }
    }

    private void SpecialAttackSODictionaryInitialize() {
        specialAttackSODictionary = new Dictionary<SpecialAttackType, SpecialAttackSO>();
        foreach (SpecialAttackSO so in SpecialAttackSOList) {
            specialAttackSODictionary.Add(so.type, so);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Player")]
public class PlayerStat : AgentStat
{
    public Stat att;
    public Stat health;
    public Stat shootSpeed;
    public Stat returnSpeed;
    public Stat attackCooldown;
    public Stat moveSpeed;

    private Dictionary<PlayerStatType, Stat> statDictionary;

    protected override void Initialize() {
        statDictionary = new Dictionary<PlayerStatType, Stat>();
        Type playerStatType = typeof(PlayerStat);

        foreach (PlayerStatType typeEnum in Enum.GetValues(typeof(PlayerStatType))) {
            try {
                string fieldName = LowerFirstChar(typeEnum.ToString());
                FieldInfo stateField = playerStatType.GetField(fieldName);
                statDictionary.Add(typeEnum, stateField.GetValue(this) as Stat);
            }
            catch (Exception e) {
                Debug.LogError($"There are no stat - {typeEnum.ToString()} {e.Message}");
            }
        }
    }

    public override void AddModifier<TEnum>(TEnum type, int value) {
        if (type is PlayerStatType) {
            statDictionary[(PlayerStatType)(object)type].AddModifier(value);
        }
    }

    public override void RemoveModifier<TEnum>(TEnum type, int value) {
        statDictionary[(PlayerStatType)(object)type].RemoveModifier(value);
    }
}
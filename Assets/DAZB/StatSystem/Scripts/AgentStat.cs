using System;
using System.Collections;
using UnityEngine;

public abstract class AgentStat : ScriptableObject {
    protected Agent owner;

    public virtual void SetOwner(Agent owner) {
        this.owner = owner;
    }

    protected virtual void OnEnable() {
        Initialize();
    }


    protected string LowerFirstChar(string input) => $"{char.ToLower(input[0])}{input[1..]}";

    public virtual void IncreaseStatFor(int value, float duration, Stat targetStat) {
        owner.StartCoroutine(StatModifyCoroutine(value, duration, targetStat));
    }

    protected IEnumerator StatModifyCoroutine(int value, float duration, Stat targetStat) {
        targetStat.AddModifier(value);
        yield return new WaitForSeconds(duration);
        targetStat.RemoveModifier(value);
    }

    protected abstract void Initialize();

    public abstract void AddModifier<TEnum>(TEnum type, int value) where TEnum : Enum;

    public abstract void RemoveModifier<TEnum>(TEnum type, int value) where TEnum : Enum;
}
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Evolution/Charging")]
public class EvolutionCharging : EvolutionEffectSO
{
    [Header("Decrease Value")]
    public int decreaseAtt;
    public int decreaseCooldown;
    public int decreaseAttackRange;

    [Header("Increase Value")]
    public int increaseMoveSpeed;

    [Header("Max Value in Charging")]
    public int maxAtt;
    public int maxAttackSpeed;
    public int maxAttackRange;
    public int maxNeckRotSpeed;
    public int maxReturnSpeed;
    

    public override void ApplyEffect() {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            AgentStat stat = PlayerManager.Instance.Head.stat;
            stat.AddModifier(PlayerStatType.Att, -decreaseAtt);
            stat.AddModifier(PlayerStatType.AttackCooldown, -decreaseCooldown);
            stat.AddModifier(PlayerStatType.AttackRange, -decreaseAttackRange);
            stat.AddModifier(PlayerStatType.MoveSpeed, increaseMoveSpeed);
        }
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        AgentStat stat = PlayerManager.Instance.Head.stat;
        stat.RemoveModifier(PlayerStatType.Att, -decreaseAtt);
        stat.RemoveModifier(PlayerStatType.AttackCooldown, -decreaseCooldown);
        stat.RemoveModifier(PlayerStatType.AttackCooldown, -decreaseAttackRange);
        stat.RemoveModifier(PlayerStatType.MoveSpeed, increaseMoveSpeed);
    }
}
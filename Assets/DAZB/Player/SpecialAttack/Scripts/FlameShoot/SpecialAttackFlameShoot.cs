using UnityEngine;

[CreateAssetMenu(menuName = "SO/SpecialAttack/FlameShoot")]
public class SpecialAttackFlameShoot : SpecialAttackSO
{
    public Flame FlamePrefab; // 나중에 풀 매니저 생기면 뺄 예정
    [Range(1, 100)] public int AppliedSpeedPercent;

    private void OnValidate() {
        type = SpecialAttackType.FlameShoot;
    }

    public override bool CanUseSpecialAttack() {
        if (isFirst) { isFirst = false; return true; }

        if (lastAttackTime + Cooldown <= Time.time) {
            return true;
        }
        else {
            return false;
        }
    }

    public override void UseSpecialAttack() {
        Flame f = Instantiate(FlamePrefab, PlayerManager.Instance.Head.transform.position, Quaternion.identity);
        f.Shoot((PlayerManager.Instance.Head.stat as PlayerStat).shootSpeed.GetValue() / (float)(AppliedSpeedPercent / 100));
        lastAttackTime = Time.time;
    }
}
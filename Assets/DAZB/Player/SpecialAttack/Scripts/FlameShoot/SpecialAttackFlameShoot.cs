using UnityEngine;

[CreateAssetMenu(menuName = "SO/SpecialAttack/FlameShoot")]
public class SpecialAttackFlameShoot : SpecialAttackSO
{
    public Flame FlamePrefab; // 나중에 풀 매니저 생기면 뺄 예정
    public float FirePower;

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
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - PlayerManager.Instance.Head.transform.position;
        f.Shoot(dir, FirePower);
        lastAttackTime = Time.time;
    }
}
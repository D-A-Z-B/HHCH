using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability/Spark")]
public class AbilitySpark : AbilityEffectSO
{
    public Spark SparkPrefab;
    public int Count;

    private void OnValidate() {
        type = AbilityType.Spark;
    }

    public override void ApplyEffect()
    {
        if (AlreadyApplied == false) {
            AlreadyApplied = true;
            AbilityManager.Instance.ApplyAbility(type);
            PlayerManager.Instance.Head.SparkEvent += SparkEventHandle;
        }
    }

    public override void RemoveEffect() {
        PlayerManager.Instance.Head.SparkEvent -= SparkEventHandle;
    }
    
    private void SparkEventHandle() {
        for (int i = 0; i < Count; ++i) {
            Spark s = Instantiate(SparkPrefab, PlayerManager.Instance.Head.transform.position, Quaternion.identity);
            s.SetDelayTime(0.25f + i * 0.15f);
        }
    }
}
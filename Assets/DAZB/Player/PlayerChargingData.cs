using UnityEngine;

[System.Serializable]
public class PlayerChargingData {
    public float currentAtt;
    public float currentAttackSpeed;
    public float currentReturnSpeed;
    public float currentAttackRange;
    public float currentNeckRotSpeed;

    public bool isFinish;

    private Agent head;

    public void Init(Agent head) {
        this.head = head;
        SetDefaultStat();
    }

    public void SetFinish(bool isFinish) {
        this.isFinish = isFinish;
    } 

    public void SetDefaultStat() {
        PlayerStat stat = head.stat as PlayerStat;
        currentAtt = stat.att.GetValue();
        currentAttackSpeed = stat.shootSpeed.GetValue();
        currentReturnSpeed = stat.returnSpeed.GetValue();
        currentAttackRange = stat.attackRange.GetValue();
    }
 }
using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public Action OnDead;
    public Action OnHit;

    private int maxHealth;
    private int currentHealth;

    private Agent owner;

    public void SetOwner(Agent agent) {
        owner = agent;
        maxHealth = (owner.stat as PlayerStat).health.GetValue();
        currentHealth = maxHealth;
    }

    public void ApplyDamage(int damage, Transform dealer, bool isknockback = false) {
        if (owner.isDead) return;

        if (isknockback) {

        }

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnHit?.Invoke();
        if (currentHealth == 0) {
            OnDead?.Invoke();
        }
        else {

        }
    }

    private void ApplyKnockback(Vector2 force) {

    }
}
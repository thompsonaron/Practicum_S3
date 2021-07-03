using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public static event Action OnEnemyDeath;
    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            // TODO animation and toss back into the pool
            OnEnemyDeath?.Invoke();
            gameObject.GetComponent<PoolableObject>().ReturnToPool();
        }
    }
    private void OnEnable()
    {
        currentHealth = totalHealth;
    }
}

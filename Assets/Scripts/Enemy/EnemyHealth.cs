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
        //GetComponent<Animator>().SetFloat("Health", currentHealth);
        if (currentHealth <= 0f)
        {
            OnEnemyDeath?.Invoke();
            gameObject.GetComponent<PoolableObject>().ReturnToPool();
        }
    }
    private void OnEnable()
    {
        currentHealth = totalHealth;
        //GetComponent<Animator>().SetFloat("Health", currentHealth);
    }
}

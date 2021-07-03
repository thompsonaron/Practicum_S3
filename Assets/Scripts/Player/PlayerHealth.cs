using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public static event Action OnPlayerDeath;
    private bool alive = true;

    public override void TakeDamage(float damage)
    {

        currentHealth -= damage;
        if (currentHealth <= 0f && alive)
        {
            alive = false;
            OnPlayerDeath?.Invoke();
        }
    }
}

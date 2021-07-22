using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected float currentHealth;
    public float totalHealth = 10;

    public abstract void TakeDamage(float damage);

    private void Start()
    {
        currentHealth = totalHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected float currentHealth;
    public float totalHealth = 3;

    public abstract void TakeDamage(float damage);
}

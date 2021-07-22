using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public static event Action OnPlayerDeath;
    private bool alive = true;

    public AudioManager audioManager;
    public Volume volume;
    public Vignette vignette;
    public Text healthText;

    private void Start()
    {
        currentHealth = totalHealth;
        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0f;
        }
    }

    private void Update()
    {
        healthText.text = currentHealth.ToString();
    }


    public override void TakeDamage(float damage)
    {
        vignette.intensity.value = 1 - (Mathf.Clamp(currentHealth, 0, totalHealth) / totalHealth);
        currentHealth -= damage;
        if (currentHealth <= 0f && alive)
        {
            Debug.Log(currentHealth);
            alive = false;
            // TODO Action call main menu or level reload or something
            OnPlayerDeath?.Invoke();
        }
    }
}
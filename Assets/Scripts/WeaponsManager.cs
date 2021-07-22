using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class WeaponsManager : MonoBehaviour
{
    public Weapon[] weapons;
    public Weapon currentWeapon;
    public int test;

    public AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (weapons.Length > 0)
        {
            currentWeapon = weapons[0];
        }
    }
    public void StopWeaponFire()
    {
        currentWeapon.StopWeaponFire();
    }



    public void EquipWeapon(WeaponType weaponType)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.weaponType == weaponType)
            {
                currentWeapon.Disable();
                currentWeapon = weapon;
                currentWeapon.Enable();
            }
        }
    }

    public void FireWeapon()
    {
        currentWeapon.FireWeapon();

       

    }
}
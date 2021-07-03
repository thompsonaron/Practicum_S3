using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public Weapon[] weapons;
    public Weapon currentWeapon;
    public int test;

    private void Start()
    {
        if (weapons.Length > 0)
        {
            currentWeapon = weapons[0];
        }
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
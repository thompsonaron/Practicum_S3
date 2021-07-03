using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameAsset projectile = GameAsset.Bullet;
    public GameObject weaponAsset;
    public float projectileVelocity = 10f;
    public float rateOfFire = 0.2f;
    private float cooldownTime = 0f;
    public Transform projectilePos;
    public WeaponType weaponType;

    public void FireWeapon()
    {
        if (cooldownTime <= 0f)
        {
            cooldownTime = rateOfFire;
            var bullet = AssetProvider.GetAsset(projectile);
            bullet.GetComponent<BulletController>().Activate(projectilePos, projectileVelocity, Target.Enemy, 1f);
            bullet.transform.position = projectilePos.position;
            bullet.transform.rotation = projectilePos.rotation;
        }
    }

    private void Update()
    {
        cooldownTime -= Time.deltaTime;
    }

    public void Enable()
    {
        weaponAsset.SetActive(true);
    }

    public void Disable()
    {
        weaponAsset.SetActive(false);
    }
}

public enum WeaponType
{
    Gun,
    MachineGun,
    Flamethrower
}

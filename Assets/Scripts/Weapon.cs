using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
    public GameAsset projectile = GameAsset.Bullet;
    public GameObject weaponAsset;
    public float projectileVelocity = 10f;
    public float rateOfFire = 0.2f;
    private float cooldownTime = 0f;
    public Transform projectilePos;
    public WeaponType weaponType;
    private bool isFlamethrowerActive = false;

    public VisualEffect flameThrowerVfx;
    public AudioManager audioManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        flameThrowerVfx = FindObjectOfType<VisualEffect>();

        var x = GameObject.Find("FlameThrowerVfxOnPlayer");
        var y = x.GetComponent<VisualEffect>();
        flameThrowerVfx.Stop();

    }


    public void FireWeapon()
    {
        if (cooldownTime <= 0f)
        {
            cooldownTime = rateOfFire;
            var bullet = AssetProvider.GetAsset(projectile);
            bullet.GetComponent<BulletController>().Activate(projectilePos, projectileVelocity, Target.Enemy, 1f);
            bullet.transform.position = projectilePos.position;
            bullet.transform.rotation = projectilePos.rotation;

            switch (weaponType)
            {
                case WeaponType.Gun:
                    audioManager.Play(ListAllAudio.W_Revolver);
                    audioManager.Stop(ListAllAudio.W_Flamethrower);
                    break;
                case WeaponType.MachineGun:
                    audioManager.Play(ListAllAudio.W_Revolver);
                    audioManager.Stop(ListAllAudio.W_Flamethrower);
                    break;

                case WeaponType.Flamethrower:
                    if (isFlamethrowerActive)
                    {
                        return;
                    }
                    isFlamethrowerActive = true;
                    flameThrowerVfx.Play();
                    audioManager.Play(ListAllAudio.W_Flamethrower);
                    break;

                    
                default:
                    break;
            }
        }

       
    }

    private void Update()
    {
        cooldownTime -= Time.deltaTime;
    }

    public void StopWeaponFire()
    {
        isFlamethrowerActive = false;
        flameThrowerVfx.Stop();
        audioManager.Stop(ListAllAudio.W_Flamethrower);
    }

    public void Enable()
    {
        if (weaponAsset != null)
        {
            weaponAsset.SetActive(true);
        }
    }

    public void Disable()
    {
        if (weaponAsset != null)
        {
            weaponAsset.SetActive(false);

        }
    }
}

public enum WeaponType
{
    Gun,
    MachineGun,
    Flamethrower
}

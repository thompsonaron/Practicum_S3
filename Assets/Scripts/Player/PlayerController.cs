using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public WeaponsManager weaponsManager;
    public DashMovement dashMovement;
    public ParticleSystem dashParticle;

    // Start is called before the first frame update
    void Start()
    {
        dashMovement = this.gameObject.AddComponent<DashMovement>();
        //register Inputs
        InputManager.OnPressedFire += RegisterShooting;
        InputManager.OnMovement += RegisterMove;
        InputManager.OnPressedSpace += OnPressedSpace;
        InputManager.OnPressedNumber += OnPressedNumber;
        InputManager.OnStoppedFire += InputManager_OnStoppedFire;
    }

    private void OnPressedNumber(int number)
    {
        switch (number)
        {
            case 1:
                weaponsManager.EquipWeapon(WeaponType.Gun);
                break;
            case 2:
                weaponsManager.EquipWeapon(WeaponType.MachineGun);
                break;
            case 3:
                weaponsManager.EquipWeapon(WeaponType.Flamethrower);
                break;
            default:
                break;
        }
    }

    private void OnPressedSpace()
    {
        dashMovement.Dash();
        dashParticle.Play();
    }

    private void RegisterShooting()
    {
        weaponsManager.FireWeapon();
    }

    private void RegisterMove(Vector3 direction)
    {
        dashMovement.SetDirection(direction);
    }

    private void InputManager_OnStoppedFire()
    {
        weaponsManager.StopWeaponFire();
    }

    void OnDestroy()
    {
        InputManager.OnPressedFire -= RegisterShooting;
        InputManager.OnMovement -= RegisterMove;
        InputManager.OnPressedSpace -= OnPressedSpace;
    }
}
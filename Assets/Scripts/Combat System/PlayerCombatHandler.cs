using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatHandler : CombatSystem
{
    public int actualWeapon { get; private set; } = 0;

    /// <summary>
    /// Script associated with the weapons inventory
    /// </summary>
    private WeaponInventories weaponsInv;

    protected override void Awake()
    {
        base.Awake();
        weaponsInv = GetComponent<WeaponInventories>();
    }

    private void Start()
    {
        WeaponEquipped = (Weapon)weaponsInv.inventory[0];    
    }

    private void Update()
    {
        if (activeDetector && Input.GetKeyDown(KeyCode.A))
        {
            shipDetector.PreviousTarget();
        }
        else if (activeDetector && Input.GetKeyDown(KeyCode.D))
        {
            shipDetector.NextTarget();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PreviousWeapon();
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NextWeapon();
        }
        

        if (PlayerInput.ShootInput && ReadyToShoot)
        {
            ShootWeapon();
        }
    }

    /// <summary>
    /// Changes weapon to the next one
    /// </summary>
    public void NextWeapon()
    {
        if (actualWeapon < weaponsInv.inventory.Length - 1)
        {
            WeaponEquipped = (Weapon)weaponsInv.inventory[actualWeapon + 1];
            actualWeapon += 1;
        }
        else
        {
            WeaponEquipped = (Weapon)weaponsInv.inventory[0];
            actualWeapon = 0;
        }
    }

    /// <summary>
    /// Changes weapon to the previous one
    /// </summary>
    public void PreviousWeapon()
    {
        if (actualWeapon > 0)
        {
            WeaponEquipped = (Weapon)weaponsInv.inventory[actualWeapon - 1];
            actualWeapon -= 1;
        }
        else
        {
            WeaponEquipped = (Weapon)weaponsInv.inventory[weaponsInv.inventory.Length - 1];
            actualWeapon = weaponsInv.inventory.Length - 1;
        }
    }
}

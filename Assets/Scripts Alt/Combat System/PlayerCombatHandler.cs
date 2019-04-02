using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatHandler : CombatSystemAlt
{
    public int actualWeapon { get; private set; } = 0;

    /// <summary>
    /// Where the weapon projectile/laser is spawned
    /// </summary>
    [SerializeField]
    private Transform shotSpawn;

    /// <summary>
    /// Script associated with the weapons inventory
    /// </summary>
    private WeaponInventories weaponsInv;

    private void Awake()
    {
        weaponsInv = GetComponent<WeaponInventories>();
    }

    private void Start()
    {
        WeaponEquipped = (Weapon)weaponsInv.inventory[0];    
    }

    private void Update()
    {
        if (PlayerInput.ShootInput && ReadyToShoot)
        {
            ShootWeapon(shotSpawn);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            NextWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            PreviousWeapon();
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

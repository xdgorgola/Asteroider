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
    }

    /// <summary>
    /// Changes weapon to the next one
    /// </summary>
    public void NextWeapon()
    {
        if (actualWeapon < weaponsInv.inventory.Length - 1)
        {
            if (weaponsInv.inventory[actualWeapon + 1] != null)
            {
                WeaponEquipped = (Weapon)weaponsInv.inventory[actualWeapon + 1]; 
            }
            else
            {
                WeaponEquipped = null;
            }
            actualWeapon += 1;
        }
        else
        {
            if (weaponsInv.inventory[0] != null)
            {
                WeaponEquipped = (Weapon)weaponsInv.inventory[0];
            }
            else
            {
                WeaponEquipped = null;         
            }
            actualWeapon = 0;
        }
    }
}

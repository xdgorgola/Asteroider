using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatHandler : CombatSystemAlt
{
    /// <summary>
    /// Where the weapon projectile/laser is spawned
    /// </summary>
    [SerializeField]
    private Transform shotSpawn;

    /// <summary>
    /// Number of equippable weapons 
    /// </summary>
    [SerializeField]
    private int numbOfWeapons = 3;

    [SerializeField]
    private int weaponEquipped = 0;

    private void Update()
    {
        if (PlayerInput.ShootInput && ReadyToShoot)
        {
            ShootWeapon(shotSpawn);
        }
    }
}

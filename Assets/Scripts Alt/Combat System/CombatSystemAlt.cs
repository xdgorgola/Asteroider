using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystemAlt : MonoBehaviour
{
    /// <summary>
    /// Indicates if the weapon can fire
    /// </summary>
    private bool readyToShoot = true;

    public bool ReadyToShoot
    {
        get { return readyToShoot; }
    }

    [SerializeField]
    private Weapon equippedWeapon = null;

    public Weapon WeaponEquipped
    {
        get { return equippedWeapon; }
        set { ChangeWeapon(value); }
    }

    [SerializeField]
    private BoltPool boltPool;
    //PoolB

    /// <summary>
    /// Function to make a player/ally/enemy shoot a bolt
    /// </summary>
    /// <param name="spawn">Where is the shot going to spawn</param>
    public void ShootBolt(Transform spawnT)
    {
        GameObject projectile = boltPool.GetFromPool();
        BoltProjectile projManager = projectile.GetComponent<BoltProjectile>();
        projManager.SpawnProjectile(spawnT, equippedWeapon.projectileSprite, equippedWeapon.projectileSpeed);
        projectile.SetActive(true);

    }

    /// <summary>
    /// Function to make a player/ally/enemy shoot its weapon
    /// </summary>
    /// <param name="spawn">Where is the shot going to spawn</param>
    public void ShootWeapon(Transform spawn)
    {
        if (equippedWeapon != null)
        {
            CheckCorrectWeaponSetup(equippedWeapon);

            if (equippedWeapon.isBolt)
            {
                ShootBolt(spawn);
                StartCoroutine(CoolDown());
            }
            else if (equippedWeapon.isContinuous)
            {
                //Not implemented yet
            }
        }
    }

    /// <summary>
    /// Changes and initializes a new weapon
    /// </summary>
    /// <param name="newWeapon">New weapon to equip</param>
    private void ChangeWeapon(Weapon newWeapon)
    {
        equippedWeapon = newWeapon;
        StartCoroutine(CoolDown());
        //Some kind of initialization for the weapon
    }

    /// <summary>
    /// Cooldown system
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoolDown()
    {
        readyToShoot = false;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(equippedWeapon.fireRate);
        Debug.Log("Reloaded!");
        readyToShoot = true;
    }

    /// <summary>
    /// Checks if a weapon is not well set up (multi-type weapon) and sends a message 
    /// </summary>
    /// <param name="toCheck">Weapon to check</param>
    private void CheckCorrectWeaponSetup(Weapon toCheck)
    {
        if (toCheck.isContinuous && toCheck.isBolt)
        {
            Debug.Log("There's something not properly set up in this weapon (CLASS OF WEAPON BOOLS).");
        }
    }
}

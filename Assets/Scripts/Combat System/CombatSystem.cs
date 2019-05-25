using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    /// <summary> Indicates if the weapon can fire </summary>
    private bool readyToShoot = true;

    public bool ReadyToShoot { get { return readyToShoot; } }

    /// <summary> Indicates if the detector is active </summary>
    protected bool activeDetector = false;

    private float fireRateModifier = 0f;

    /// <summary> Equipped weapon </summary>
    [SerializeField]
    private Weapon equippedWeapon = null;

    public Weapon WeaponEquipped
    {
        get { return equippedWeapon; }
        set { ChangeWeapon(value); }
    }

    /// <summary> Bolt Pool </summary>
    [SerializeField]
    private BoltPool boltPool;
    /// <summary> Missile Pool </summary>
    [SerializeField]
    private MissilePool missilePool;

    private ShipStats ship;

    /// <summary> Ship detector associated to GameObject </summary>
    protected ShipDetector shipDetector;

    /// <summary> Projectiles/Continuous spawn point </summary>
    [SerializeField]
    protected Transform shotSpawn;

    protected virtual void Awake()
    {
        GameObject poolManager = GameObject.FindGameObjectWithTag("Pool Manager");
        boltPool = poolManager.GetComponent<BoltPool>();
        missilePool = poolManager.GetComponent<MissilePool>();
        shipDetector = GetComponentInChildren<ShipDetector>();
        ship = GetComponent<ShipStats>();

        ship.onPartsChange.AddListener(UpdateFireRate);
    }

    void UpdateFireRate()
    {
        fireRateModifier = ship.fireRate;
    }

    /// <summary> Function to make a player/ally/enemy shoot a bolt </summary>
    void ShootBolt()
    {
        GameObject projectile = boltPool.GetFromPool();
        BoltProjectile projManager = projectile.GetComponent<BoltProjectile>();
        //projManager.SpawnProjectile(spawnT, equippedWeapon.projectileSprite, equippedWeapon.projectileSpeed);
        projManager.SpawnProjectile(shotSpawn, equippedWeapon as Bolt);
    }

    /// <summary> Function to make a player/ally/enemy shoot a misisle </summary>
    void ShootMissile()
    {
        GameObject missile = missilePool.GetMissileFromPool();
        MissileMover missileSpawner = missile.GetComponent<MissileMover>();
        missileSpawner.InitializeMissileMovement(equippedWeapon as Missile);
        Transform target = shipDetector.GetTarget();
        if (target != null)
        {
            missileSpawner.SpawnTargetedMissile(target, shotSpawn);
        }
        else
        {
            missileSpawner.SpawnUnTargetedMissile(shotSpawn);
        }
    }

    /// <summary> Function to make a player/ally/enemy shoot its weapon </summary>
    public void ShootWeapon()
    {
        if (equippedWeapon != null)
        {
            //CheckCorrectWeaponSetup(equippedWeapon);
            if (equippedWeapon.GetType() == typeof(Bolt))
            {
                ShootBolt();
            }
            else if (equippedWeapon.GetType() == typeof(Missile))
            {
                ShootMissile();
            }
            //else if (equippedWeapon.isContinuous)
            //{
            //    //Not implemented yet
            //}
            StartCoroutine(CoolDown());
        }
    }

    /// <summary> Changes and initializes a new weapon </summary>
    /// <param name="newWeapon">New weapon to equip</param>
    private void ChangeWeapon(Weapon newWeapon)
    {
        activeDetector = false;
        shipDetector.enabled = false;
        shipDetector.drawDebug = false;
        equippedWeapon = newWeapon;
        StartCoroutine(CoolDown());
        if (newWeapon != null)
        {
            if (newWeapon.GetType() == typeof(Missile))
            {
                activeDetector = true;
                shipDetector.enabled = true;
                shipDetector.drawDebug = true;
            }
        }      
    }

    /// <summary> Cooldown system </summary>
    IEnumerator CoolDown()
    {
        readyToShoot = false;
        //Debug.Log("Reloading...");
        if (equippedWeapon != null)
        {
            float finalCD = equippedWeapon.fireRate - equippedWeapon.fireRate * fireRateModifier;
            if (finalCD < 0.25) finalCD = 0.25f;
            Debug.Log(finalCD);
            yield return new WaitForSeconds(finalCD);
        }
        //Debug.Log("Reloaded!");
        readyToShoot = true;
    }
}

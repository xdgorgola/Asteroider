using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSyst : MonoBehaviour
{

    public int equippedWeapon = 0;
    public int maxWeapons = 3;
    [HideInInspector]
    public float shootTimer = 0f;
    [HideInInspector]
    public float weaponCD;

    public LaserBolt[] invWeapons;
    public Transform shotSpawn;

    KeyCode nextWP = KeyCode.E;
    KeyCode prvWP = KeyCode.Q;
    KeyCode fire = KeyCode.Space;

    private void Awake()
    {
        weaponCD = invWeapons[equippedWeapon].fireRate;
    }

    // Update is called once per frame
    void Update()
    {

        shootTimer += (shootTimer < weaponCD) ? Time.deltaTime : 0;
        if (Input.GetKey(fire) && shootTimer >= weaponCD)
        {
            invWeapons[equippedWeapon].Shoot(projectileSpawn: shotSpawn);
            shootTimer = 0;
        }
        if (Input.GetKeyDown(nextWP))
        {
            NextWeapon();
        }
        if (Input.GetKeyDown(prvWP))
        {
            PreviousWeapon();
        }
    }

    //public void Shoot()
    //{
    //    //GameObject proj = PlayerProjectilePool.playerPPool.GetFromPool();
    //    //proj.GetComponent<SimpleProjectileMover>().SpawnProjectile(spawnTrans: shotSpawn, spr: invWeapons[equippedWeapon].projectileSprite, speed: invWeapons[equippedWeapon].projectileSpeed);
    //    invWeapons[equippedWeapon].Shoot();
    //}
    
    public void UpdateWeapon(LaserBolt weapon)
    {
        shootTimer = 0;
        weaponCD = weapon.fireRate;
    }

    public void NextWeapon()
    {
        if(equippedWeapon < invWeapons.Length - 1)
        {
            equippedWeapon += 1;
        }
        else
        {
            equippedWeapon = 0;
        }
        UpdateWeapon(weapon: invWeapons[equippedWeapon]);
    }

    public void PreviousWeapon()
    {
        if (equippedWeapon > 0)
        {
            equippedWeapon -= 1;
        }
        else
        {
            equippedWeapon = maxWeapons - 1;
        }
        UpdateWeapon(weapon: invWeapons[equippedWeapon]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bolt_", menuName = "Weapon/Laser Bolt", order = 1)]
public class LaserBolt : Item
{
    public int damage;
    public float fireRate;
    public float projectileSpeed;

    public Sprite projectileSprite;

    public void Shoot(Transform projectileSpawn)
    {
        GameObject obj = PlayerProjectilePool.playerPPool.GetFromPool();
        obj.GetComponent<SimpleProjectileMover>().SpawnProjectile(spawnTrans: projectileSpawn, spr: projectileSprite, speed: projectileSpeed);
    }
}

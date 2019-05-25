using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Part_", menuName = "Ship Part")]
public class ShipPart :Item
{
    public int health;
    public int shield;
    public float shieldRechargeRate;
    public float speed;
    [Range(0,1)]
    public float fireRate;
}

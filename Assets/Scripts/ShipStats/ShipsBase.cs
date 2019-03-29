using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship_", menuName = "Ship")]
public class ShipsBase : ScriptableObject
{
    public int health;
    public int shield;
    public float shieldRechargeRate;
    public float speed;
    public float fireRate;
}
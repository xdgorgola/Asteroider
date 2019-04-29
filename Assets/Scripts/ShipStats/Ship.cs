using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship")]
public class Ship : ScriptableObject
{
    public int health;
    public int shield;
    public float shieldRechargeRate;
    public float speed;
    public float fireRate;

    public Sprite shipSprites;
}

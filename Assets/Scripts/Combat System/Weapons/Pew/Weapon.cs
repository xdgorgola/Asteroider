using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Weapon", order = 1)]
public class Weapon : Item
{
    public int damage;
    public float fireRate;
    public float projectileSpeed;

    public Sprite projectileSprite;

    //bool for types, one must be true while the others must be set to false.
    public bool isBolt;
    public bool isContinuous;
}

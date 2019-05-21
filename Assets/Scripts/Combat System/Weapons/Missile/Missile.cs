using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missile", menuName = "Weapon/Missile", order = 2)]
public class Missile : Weapon
{
    public float missileDamage = 20f;
    public float detectionRange = 20f;
    public float explosionRange = 10f;
    public float knockBack = 10f;
    public float speed = 30f;

    public Sprite missileSprite = null;
}

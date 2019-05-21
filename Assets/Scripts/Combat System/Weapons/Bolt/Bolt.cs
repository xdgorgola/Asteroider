using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bolt", menuName = "Weapon/Bolt", order = 3)]
public class Bolt : Weapon
{
    public float boltDamage = 10f;
    public float speed = 30f;

    public Sprite boltSprite = null;
}


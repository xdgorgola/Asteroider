using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missile", menuName = "Weapon/Missile", order = 2)]
public class Missile : Weapon
{
    public float range = 20f;
    public float speed = 30f;
}

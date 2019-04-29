using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipStats : MonoBehaviour
{
    [SerializeField]
    private Ship baseShip;

    private int health;
    private int shield;
    private float shieldRechargeRate;
    public float ShipSpeed
    {
        get { return speed; }
    }
    private float speed;
    private float fireRate;

    private Sprite shipSprites;
    private ShipPartsInventory shipParts;

    private void Awake()
    {
        shipParts = GetComponent<ShipPartsInventory>();
        shipParts.onInventoryChange.AddListener(InitializeParts);
    }
    private void Start()
    {
        InitializeShip();
    }

    void InitializeShip()
    {
        if (baseShip == null)
        {
            Debug.LogError("CHAMITO ESTA NAVE NO PUEDE SER NULL");
        }
        health = baseShip.health;
        shield = baseShip.shield;
        shieldRechargeRate = baseShip.shieldRechargeRate;
        speed = baseShip.speed;
        shieldRechargeRate = baseShip.shieldRechargeRate;
        fireRate = baseShip.fireRate;

        shipSprites = baseShip.shipSprites;
    }

    void InitializeParts()
    {
        Item[] parts = shipParts.INV;
        foreach(ShipPart part in parts)
        {
            InitializePart(part);
        }
    }

    void InitializePart(ShipPart part)
    {
        InitializeShip();
        health += part.health;
        shield += part.shield;
        shieldRechargeRate += part.shieldRechargeRate;
        speed += part.speed;
        shieldRechargeRate += part.shieldRechargeRate;
        fireRate += part.fireRate;
    }
}

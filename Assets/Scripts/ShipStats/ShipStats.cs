using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipPartsEvents : UnityEvent { }

[RequireComponent(typeof(HealthManager), typeof(ShipPartsInventory))]
public class ShipStats : MonoBehaviour
{
    [SerializeField]
    private Ship baseShip;

    /// <summary>
    /// Ship max health
    /// </summary>
    public float maxHealth;
    public float shield;
    public float shieldRechargeRate;
    public float ShipSpeed
    {
        get { return speed; }
    }
    private float speed;
    public float fireRate;

    private Sprite shipSprites;
    private ShipPartsInventory shipParts;

    private PartStats partStats = new PartStats();

    public ShipPartsEvents onPartsChange = new ShipPartsEvents();

    private void Awake()
    {
        shipParts = GetComponent<ShipPartsInventory>();
        shipParts.onPartChange.AddListener(InitializeParts);
    }
    private void Start()
    {
        InitializeShip();
        InitializeParts();
        GetComponent<HealthManager>().UpdateHealthManager();
    }

    /// <summary>
    /// Initialize ship base stats
    /// </summary>
    void InitializeShip()
    {
        if (baseShip == null)
        {
            Debug.LogError("CHAMITO ESTA NAVE NO PUEDE SER NULL");
        }
        maxHealth = baseShip.health;
        //Debug.Log(maxHealth);
        //Debug.Log(baseShip.health);
        shield = baseShip.shield;
        shieldRechargeRate = baseShip.shieldRechargeRate;
        speed = baseShip.speed;
        fireRate = baseShip.fireRate;

        //Debug.Log("-----------------------------------------------------------------------------");
        //Debug.Log("Nave Base.");
        //Debug.Log("Max Health: " + maxHealth);
        //Debug.Log("Shield: " + shield);
        //Debug.Log("Recharge Rate: "+ shieldRechargeRate);
        //Debug.Log("Speed: " + speed);
        //Debug.Log("Fire Rate: " + fireRate);
        //Debug.Log("-----------------------------------------------------------------------------");

        shipSprites = baseShip.shipSprites;
    }

    /// <summary>
    /// Apply parts stats to ship
    /// </summary>
    void InitializeParts()
    {
        Item[] parts = shipParts.INV;
        InitializeShip();
        partStats.Reset();
        foreach (ShipPart part in parts)
        {
            if (part != null)
            {
                Debug.Log(part.name);
                InitializePart(part);
            }
        }
        maxHealth += partStats.MaxHealth;
        shield += partStats.Shield;
        shieldRechargeRate += partStats.ShieldRechargeRate;
        speed += partStats.Speed;
        fireRate += partStats.FireRate;
        onPartsChange.Invoke();
    }

    /// <summary>
    /// Apply part stats to ship
    /// </summary>
    /// <param name="part">Part to apply</param>
    void InitializePart(ShipPart part)
    {
        partStats.MaxHealth += part.health;
        partStats.Shield += part.shield;
        partStats.ShieldRechargeRate += part.shieldRechargeRate;
        partStats.Speed += part.speed;
        partStats.FireRate += part.fireRate;

        //Debug.Log("-----------------------------------------------------------------------------");
        //Debug.Log("Con partes.");
        //Debug.Log("New Max Health: " + maxHealth);
        //Debug.Log("New Shield: " + shield);
        //Debug.Log("New Recharge Rate: " + shieldRechargeRate);
        //Debug.Log("New Speed: " + speed);
        //Debug.Log("New Fire Rate: " + fireRate);
        //Debug.Log("-----------------------------------------------------------------------------");
    }

    public struct PartStats
    {
        public float MaxHealth;
        public float Shield;
        public float ShieldRechargeRate;
        public float Speed;
        public float FireRate;

        public PartStats(float maxHealth = 0, float shield = 0, float shieldRechargeRate = 0, float speed = 0, float fireRate = 0)
        {
            MaxHealth = Shield = ShieldRechargeRate = Speed = FireRate = 0;
        }

        public void Reset()
        {
            MaxHealth = Shield = ShieldRechargeRate = Speed = FireRate = 0;
        }
    }
}

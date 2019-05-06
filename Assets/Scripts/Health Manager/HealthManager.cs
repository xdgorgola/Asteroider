using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeEvents : UnityEvent { }
//Se puede usar un evento que si, cuando se acabe el escudo, spawnee una explosion
public class ShieldEvents : UnityEvent { }
public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;
    public float Health { get { return health; } }
    private float health = 100f;

    [SerializeField]
    private float maxShield = 30;
    public float Shield { get { return shield; } }
    [SerializeField]
    private float shield = 30f;
    [SerializeField]
    private float shieldRechargePS = 4f;
    [SerializeField]
    private float rechargeTimeInterval = 0.04f;
    private float shieldRechargeAmount;
    [SerializeField]
    private float shieldDelayTime = 2f;
    private IEnumerator rechargeShield;

    /// <summary>
    /// Evento invocado cuando hay un cambio en la vida
    /// </summary>
    public LifeEvents onLifeChange = new LifeEvents();
    public ShieldEvents onShieldChange = new ShieldEvents();
    public ShieldEvents onShieldCharge = new ShieldEvents();
    public ShieldEvents onShieldDeplete = new ShieldEvents();

    private ShipStats ship;

    //Una lista con las partes

    private void Awake()
    {
        //shipParts = GetComponent<ShipPartsInventory>();
        ship = GetComponent<ShipStats>();
        ship.onPartsChange.AddListener(UpdateHealthManager);
    }

    private void Start ()
    {
        CalculateSomethingShield();
        //UpdateHealthManager();
        //health = maxHealth;
        //shield = maxShield;
    }

    public void UpdateHealthManager()
    {
        maxHealth = ship.maxHealth;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        maxShield = ship.shield;
        shieldRechargePS = ship.shieldRechargeRate;
        CalculateSomethingShield();
        if (shield >= maxShield)
        {
            shield = maxShield;
        }
        else
        {
            rechargeShield = StartShieldReload();
            StartCoroutine(rechargeShield);
        }
        onLifeChange.Invoke();
        onShieldChange.Invoke();
    }

    private void CalculateSomethingShield()
    {
        shieldRechargeAmount = shieldRechargePS * rechargeTimeInterval;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            TakeDamage(10f);
        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
        {
            ReduceHealth(10f);
        }
    }

    public void AddHealth(float add)
    {
        if (health + add >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += add;
        }
        onLifeChange.Invoke();
    }

    public void ReduceHealth(float reduce)
    {
        if (health - reduce <= 0)
        {
            health = 0;
        }
        else
        {
            health -= reduce;
        }
        onLifeChange.Invoke();
    }

    public void AddShield(float add)
    {
        if (shield + add >= maxShield)
        {
            shield = maxShield;
            onShieldCharge.Invoke();
        }
        else
        {
            shield += add;
        }
        Debug.Log(shield);
        onShieldChange.Invoke();
    }

    public void ReduceShield(float reduce)
    {
        if (shield - reduce <= 0)
        {
            shield = 0;
            onShieldDeplete.Invoke();
        }
        else
        {
            shield -= reduce;
        }
        if (rechargeShield != null)
        {
            StopCoroutine(rechargeShield);
        }
        rechargeShield = StartShieldReload();
        StartCoroutine(rechargeShield);
        onShieldChange.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (shield >= damage)
        {
            ReduceShield(damage);
        }
        else
        {
            damage -= shield;
            ReduceShield(shield);
            ReduceHealth(damage);
        }
    }

    public IEnumerator StartShieldReload()
    {
        yield return new WaitForSeconds(shieldDelayTime);
        while (shield != maxShield)
        {
            AddShield(shieldRechargeAmount);
            yield return new WaitForSeconds(rechargeTimeInterval);
        }
    }

    void RoundToTwo(ref float toRound)
    {
        toRound = Mathf.Floor(toRound * 100) / 100;
    }
}

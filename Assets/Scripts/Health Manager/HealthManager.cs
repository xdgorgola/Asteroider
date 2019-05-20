using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeEvents : UnityEvent { }
//Se puede usar un evento que si, cuando se acabe el escudo, spawnee una explosion
public class ShieldEvents : UnityEvent { }
public class HealthManager : MonoBehaviour
{
    /// <summary> Ship max health </summary>
    [SerializeField]
    private float maxHealth = 100;
    /// <summary> Actual ship health </summary>
    public float Health { get { return health; } }
    /// <summary> Actual ship health </summary>
    private float health = 100f;

    /// <summary> Ship max shield </summary>
    [SerializeField]
    private float maxShield = 30;
    /// <summary> Actual shield </summary>
    public float Shield { get { return shield; } }
    /// <summary> Actual shield </summary>
    [SerializeField]
    private float shield = 30f;
    /// <summary> Shield recharge amount per second </summary>
    [SerializeField]
    private float shieldRechargePS = 4f;
    /// <summary> Time intervals where shield recharge is applied </summary>
    [SerializeField]
    private float rechargeTimeInterval = 0.04f;
    /// <summary> How much is the shield recharged every time interval </summary>
    private float shieldRechargeAmount;
    /// <summary> Delay time before starting shield reload </summary>
    [SerializeField]
    private float shieldDelayTime = 2f;
    /// <summary> Progressive shield recharge coroutine </summary>
    private IEnumerator rechargeShield;

    /// <summary> Event invoked when the life changes </summary>
    public LifeEvents onLifeChange = new LifeEvents();
    /// <summary> Event invoked when ship health reachs 0 </summary>
    public LifeEvents onLifeDeplete = new LifeEvents();
    /// <summary> Event invoked when the shield changes </summary>
    public ShieldEvents onShieldChange = new ShieldEvents();
    /// <summary> Event invoked when shield is shield finishes chargin </summary>
    public ShieldEvents onShieldCharge = new ShieldEvents();
    /// <summary> Event invoked when the shield reachs 0 </summary>
    public ShieldEvents onShieldDeplete = new ShieldEvents();

    /// <summary> Ship stats </summary>
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

    /// <summary> Updates health/shield related variables with the ship stats </summary>
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

    /// <summary> Calculates shield recharge amount per time interval </summary>
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

    /// <summary> Adds health </summary>
    /// <param name="add">Health amount to add</param>
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
    
    /// <summary> Reduces health </summary>
    /// <param name="reduce">Health amount to reduce</param>
    public void ReduceHealth(float reduce)
    {
        if (health - reduce <= 0)
        {
            health = 0;
            onLifeDeplete.Invoke();
        }
        else
        {
            health -= reduce;
        }
        onLifeChange.Invoke();
    }

    /// <summary> Adds shield </summary>
    /// <param name="add">Shield amount to add</param>
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

    /// <summary> Reduces shield </summary>
    /// <param name="reduce">Shield amount to reduce</param>
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

    /// <summary> Handles damage. Reduces health and shield </summary>
    /// <param name="damage">Damage to take</param>
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

    /// <summary> Starts shield reloading process. Charges shield</summary>
    public IEnumerator StartShieldReload()
    {
        yield return new WaitForSeconds(shieldDelayTime);
        while (shield != maxShield)
        {
            AddShield(shieldRechargeAmount);
            yield return new WaitForSeconds(rechargeTimeInterval);
        }
    }

    /// <summary> Daniel's stupid method </summary>
    /// <param name="toRound">Love you dan</param>
    void RoundToTwo(ref float toRound)
    {
        toRound = Mathf.Floor(toRound * 100) / 100;
    }
}

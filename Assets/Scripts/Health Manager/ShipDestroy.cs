using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject lootDebris;

    private HealthManager healthManager;

    /// <summary> Ship inventory </summary>
    private Inventory shipInventory;
    /// <summary> Is this ship looteable? </summary>
    private bool looteable = true;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();    
        if (looteable)
        {
            shipInventory = GetComponent<Inventory>();
        }
    }

    private void Start()
    {
        healthManager.onLifeDeplete.AddListener(DestroyShip);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            healthManager.TakeDamage(1000);
        }
    }

    public void DestroyShip()
    {
        Debug.Log("KAPOW SHIP");
        if (looteable)
        {
            Debug.Log("Dropping loot");
        }
        Destroy(gameObject);
    }
}

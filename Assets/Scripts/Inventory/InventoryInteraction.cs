using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InventoryInteraction : MonoBehaviour
{
    /// <summary>
    /// Which inventory in inventories the player is going 
    /// to interact next
    /// </summary>
    [SerializeField]
    private int actualInv = 0;

    /// <summary>
    /// Indicates if the player is interacting with another inventory
    /// </summary>
    private bool interacting = false;

    /// <summary>
    /// Inventories in range
    /// </summary>
    [SerializeField]
    private List<Inventory> inventories;
    /// <summary>
    /// Player inventory
    /// </summary>
    private Inventory playerInv;
    /// <summary>
    /// Player weapons inventories
    /// </summary>
    private Inventory weaponsInv;

    private void Awake()
    {
        playerInv = GetComponent<Inventory>();
        weaponsInv = GetComponent<WeaponInventories>();
        inventories = new List<Inventory>();
    }

    //ISSUES
    //FIXED-PARTIALLY(Keep testing for cases): Open normal inv and weapons inv, then open the loot inv, then close the normal inv and finally try
    //to open the loot inv, it tries to remove an unknown inventory.
    
    //TO-DO
    //The last inventory FIX (isOpen bool) is not implemented to interactions inventories conditions (interacting bool still here)

    private void Update()
    {
        if (inventories.Count == 0)
        {
            actualInv = 0;
        }
        if (PlayerInput.InteractionDown && inventories.Count > 0 && !interacting)
        {
            interacting = true;
            InventorySystem.invSyst.OpenInventory(inventories[actualInv]);
        }
        else if (PlayerInput.InteractionDown && inventories.Count > 0 && interacting)
        {
            interacting = false;
            InventorySystem.invSyst.CloseInventory(inventories[actualInv]);
            if (actualInv < inventories.Count - 1)
            {
                actualInv += 1;
            }
            else
            {
                actualInv = 0;
            }
        }

        //Opening single inventories block start
        if (PlayerInput.InventoryDown)
        {
            if (!CheckInvStatus(playerInv))
            {
                Debug.Log("Opening player inv...");
                InventorySystem.invSyst.OpenInventory(playerInv);
            }
            else
            {
                Debug.Log("Closing player inv...");
                InventorySystem.invSyst.CloseInventory(playerInv);
            }
        }
        else if (PlayerInput.WeaponInventoryDown)
        {
            if (!CheckInvStatus(weaponsInv))
            {
                Debug.Log("Opening weapons inv...");
                InventorySystem.invSyst.OpenInventory(weaponsInv);
            }
            else
            {
                Debug.Log("Closing weapons inv...");
                InventorySystem.invSyst.CloseInventory(weaponsInv);
            }
        }
        //Opening single inventories block end
    }

    /// <summary>
    /// Removes inventory from the inventories in ranges.
    /// It also updates the actual inv
    /// </summary>
    /// <param name="toRemove">Inventory to be removed</param>
    public void RemoveInventory(Inventory toRemove)
    {
        inventories.Remove(toRemove);
        if (inventories.Count != 0)
        {
            if (actualInv > inventories.Count - 1)
            {
                actualInv = inventories.Count - 1;
            }
        }
        else
        {
            actualInv = 0;
        }
    }

    /// <summary>
    /// Check if inventory is open.
    /// </summary>
    /// <param name="invToCheck">Inventory to check.</param>
    /// <returns>Inventory isOpen bool</returns>
    public bool CheckInvStatus(Inventory invToCheck)
    {
        return invToCheck.IsOpen;
    }

    //Here we add the inventory to the invetories list just when the players gets in range
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Test_Interaction"))
        {
            inventories.Add(collision.gameObject.GetComponentInParent<Inventory>());
        }
    }

    //Here the collision inventory is removed when it gets out of range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Test_Interaction"))
        {
            RemoveInventory(collision.gameObject.GetComponentInParent<Inventory>());
        }
    }

}

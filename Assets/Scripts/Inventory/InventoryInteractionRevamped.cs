using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InventoryInteractionRevamped : MonoBehaviour
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
    /// Indicates if the player got its inventory open
    /// </summary>
    private bool openInventory = false;

    /// <summary>
    /// Inventories in range
    /// </summary>
    [SerializeField]
    private List<Inventory> inventories;
    /// <summary>
    /// Player inventory
    /// </summary>
    private Inventory playerInv;
    private Inventory weaponsInv;

    private void Awake()
    {
        playerInv = GetComponent<Inventory>();
        weaponsInv = GetComponent<WeaponInventories>();
        inventories = new List<Inventory>();
    }

    private void Update()
    {
        if (inventories.Count == 0)
        {
            actualInv = 0;
        }
        if (PlayerInput.InteractionDown && inventories.Count > 0 && !interacting)
        {
            interacting = true;
            InventorySystem.invSyst.StartInvInteraction(playerInv, inventories[actualInv]);
        }

        else if (PlayerInput.InteractionDown && inventories.Count > 0 && interacting)
        {
            interacting = false;
            InventorySystem.invSyst.StopInvInteraction();
            if (actualInv < inventories.Count - 1)
            {
                actualInv += 1;
            }
            else
            {
                actualInv = 0;
            }
        }

        if (PlayerInput.InventoryDown)
        {

            if (!openInventory)
            {
                InventorySystem.invSyst.StartInvInteraction(playerInv);
            }
            else
            {
                InventorySystem.invSyst.StopInvInteraction();
            }
            openInventory = !openInventory;
        }

        else if (PlayerInput.WeaponInventoryDown)
        {
            if (!openInventory)
            {
                InventorySystem.invSyst.StartInvInteraction(weaponsInv);
            }
            else
            {
                InventorySystem.invSyst.StopInvInteraction();
            }
            openInventory = !openInventory;
        }

    }
    
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Test_Interaction"))
        {
            inventories.Add(collision.gameObject.GetComponentInParent<Inventory>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Test_Interaction"))
        {
            RemoveInventory(collision.gameObject.GetComponentInParent<Inventory>());
        }
    }

}

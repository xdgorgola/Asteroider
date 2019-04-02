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
    /// Indicates if the player got its weapons inventory open
    /// </summary>
    private bool openWeapons = false;

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

    private void Update()
    {
        if (inventories.Count == 0)
        {
            actualInv = 0;
        }
        if (PlayerInput.InteractionDown && inventories.Count > 0 && !interacting)
        {
            interacting = true;
            //InventorySystem.invSyst.StartInvInteraction(playerInv, inventories[actualInv]);
            InventorySystem.invSyst.AddInteraction(inventories[actualInv]);
        }

        else if (PlayerInput.InteractionDown && inventories.Count > 0 && interacting)
        {
            interacting = false;
            //InventorySystem.invSyst.StopInvInteraction();
            InventorySystem.invSyst.RemoveInteraction(inventories[actualInv]);
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

            if (!openInventory)
            {
                InventorySystem.invSyst.AddInteraction(playerInv);
                //InventorySystem.invSyst.StartInvInteraction(playerInv);
            }
            else
            {
                InventorySystem.invSyst.RemoveInteraction(playerInv);
                //InventorySystem.invSyst.StopInvInteraction();
            }
            openInventory = !openInventory;
        }

        else if (PlayerInput.WeaponInventoryDown)
        {
            if (!openWeapons)
            {
                InventorySystem.invSyst.AddInteraction(weaponsInv);
                //InventorySystem.invSyst.StartInvInteraction(weaponsInv);
            }
            else
            {
                InventorySystem.invSyst.RemoveInteraction(weaponsInv);
                //InventorySystem.invSyst.StopInvInteraction();
            }
            openWeapons = !openWeapons;
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

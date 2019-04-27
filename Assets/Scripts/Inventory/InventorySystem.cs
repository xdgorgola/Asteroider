using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem invSyst;

    /// <summary>
    /// Involved inventory 1
    /// </summary>
    [SerializeField]
    private Inventory inv1;
    /// <summary>
    /// Involved inventory 2
    /// </summary>
    [SerializeField]
    private Inventory inv2;

    /// <summary>
    /// Selected slots from inventories
    /// </summary>
    private InventorySlot selSlot1;
    private InventorySlot selSlot2;

    /// <summary>
    /// GameObjects related to the inventories UI
    /// </summary>
    [SerializeField]
    private GameObject inv1UI;
    [SerializeField]
    private GameObject inv2UI;

    /// <summary>
    /// GameObjects related to the inventories slot
    /// </summary>
    private GameObject[] inv1Slots;
    private GameObject[] inv2Slots;

    private void Awake()
    {
        invSyst = this;   
    }

    /// <summary>
    /// Starts the interaction with invToAdd inventory
    /// </summary>
    /// <param name="invToAdd">Inventory to add interaction</param>
    public void AddInteraction(Inventory invToAdd)
    {
        Debug.Log("Starting AddInteraction() method");
        //If inv1 is available
        if (inv1 == null && inv2 == null)
        {
            Debug.Log("Adding" + invToAdd);
            invToAdd.IsOpen = true;
            inv1 = invToAdd;
            inv1UI = inv1.invObject;
            inv1UI.SetActive(true);
            inv1.InitializeInventory();
        }
        else if (inv1 == null && inv2 != null)
        {
            Debug.Log("Adding" + invToAdd);
            invToAdd.IsOpen = true;
            inv1 = invToAdd;
            inv1UI = inv1.invObject;
            inv1UI.SetActive(true);
            inv1.InitializeInventory();
        }
        //If inv2 is available
        else if (inv1 != null && inv2 == null)
        {
            Debug.Log("Adding" + invToAdd);
            invToAdd.IsOpen = true;
            inv2 = invToAdd;
            inv2UI = inv2.invObject;
            inv2UI.SetActive(true);
            inv2.InitializeInventory();
        }
        //If neither of the invs are availables
        else if (inv1 != null && inv2 != null)
        {
            Debug.Log("The bug is consecuence of changing openInventory, openWeapons when some inventory cannot be oppened or is replaced by another. ");
            Debug.Log("Returning a bool to check if the function made it purpose can solve this.");
            RemoveInteraction(inv1);
            AddInteraction(invToAdd);
        }
        InitializeSlots();
    }

    /// <summary>
    /// Stops the interaction with invtoRemove inventory
    /// </summary>
    /// <param name="invToRemove">Inventory to stop interacting with</param>
    public void RemoveInteraction(Inventory invToRemove)
    {
        invToRemove.IsOpen = false;
        //If invToRemove is at inv1
        if (invToRemove == inv1)
        {
            inv1 = null;
            inv1UI.SetActive(false);
            //inv1UI = null; //It could be left active to access faster next time
        }
        //If invToRemove is at inv2
        else if (invToRemove == inv2)
        {
            inv2 = null;
            inv2UI.SetActive(false);
            //inv2UI = null; //It could be left active to access faster next time
        }
        else
        {
            Debug.LogError("The inventory to remove isn't an active inventory in InventorySystem.cs!");
        }
    }

    /// <summary>
    /// Method to add an item to a slot
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="slot">Slot to modify</param>
    public void AddItemToSlot(Item item, InventorySlot slot)
    {
        //slot.AddItemToSlot(item);
        //Checks which inventory the slot belongs to
        if(slot.transform.parent.gameObject == inv1UI)
        {
            int pos = System.Array.IndexOf(inv1Slots, slot.gameObject);
            inv1.AddItem(pos, item, slot);            
        }
        else
        {
            int pos = System.Array.IndexOf(inv2Slots, slot.gameObject);
            inv2.AddItem(pos, item, slot);
        }
    }

    /// <summary>
    /// Method to empty a slot
    /// </summary>
    /// <param name="slot">Slot to empty</param>
    public void RemoveItemFromSlot(InventorySlot slot)
    {
        slot.RemoveItemFromSlot();
        //Checks which inventory the slot belongs to

        if (slot.transform.parent.gameObject == inv1UI)
        {
            int pos = System.Array.IndexOf(inv1Slots, slot.gameObject);
            inv1.RemoveItem(pos);
        }
        else
        {
            int pos = System.Array.IndexOf(inv2Slots, slot.gameObject);
            inv2.RemoveItem(pos);
        }
    }

    /// <summary>
    /// Method to select a slot
    /// </summary>
    /// <param name="slotG">Slot to select</param>
    public void SelectSlot(GameObject slotG)
    {
        Debug.Log("Seleccionando slot");
        InventorySlot slot = slotG.GetComponent<InventorySlot>();
        //If the slot isn't selected, now it is
        if (!slot.isSelected)
        {
            if (selSlot1 == null)
            {
                slot.isSelected = true;
                selSlot1 = slot;
                ExchangeItems();
                return;
            }
            if (selSlot2 == null)
            {
                slot.isSelected = true;
                selSlot2 = slot;
                ExchangeItems();
                return;
            }
            Debug.Log("There is already two slots selected");
        }
        //If the slot is selected, now it isn't
        if (slot.isSelected)
        {
            slot.isSelected = false;
            if (selSlot1 == slot)
            {
                selSlot1 = null;
                return;
            }
            if (selSlot2 == slot)
            {
                selSlot2 = null;
                return;
            }
        }
    }

    /// <summary>
    /// Method to initialize the arrays containing the GameObjects associated with the slots
    /// </summary>
    public void InitializeSlots()
    {
        if (inv1 != null)
        {
            inv1Slots = inv1.inventorySlots;
        }
        if (inv2 != null)
        {
            inv2Slots = inv2.inventorySlots;
        }
    }

    /// <summary>
    /// Method to exchange items between the selected slots
    /// </summary>
    public void ExchangeItems()
    {
        if (selSlot1 != null && selSlot2 != null)
        {
            Item temp = selSlot1.slotItem;
            AddItemToSlot(selSlot2.slotItem, selSlot1);
            AddItemToSlot(temp, selSlot2);

            selSlot1.isSelected = selSlot2.isSelected = false;
            selSlot1 = selSlot2 = null;
        }
    }
}

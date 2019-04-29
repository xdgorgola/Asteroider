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
    public void OpenInventory(Inventory invToAdd)
    {
        invToAdd.InitializeInventory();
        invToAdd.IsOpen = true;
        invToAdd.invObject.SetActive(true);
    }

    /// <summary>
    /// Stops the interaction with invtoRemove inventory
    /// </summary>
    /// <param name="invToRemove">Inventory to stop interacting with</param>
    public void CloseInventory(Inventory invToRemove)
    {
        invToRemove.IsOpen = false;
        invToRemove.invObject.SetActive(false);
        //If invToRemove is at inv1
        if (invToRemove == inv1)
        {
            inv1 = null;
            inv1UI.SetActive(false);
            inv1UI = null; //It could be left active to access faster next time
        }
        //If invToRemove is at inv2
        else if (invToRemove == inv2)
        {
            inv2 = null;
            inv2UI.SetActive(false);
            inv2UI = null; //It could be left active to access faster next time
        }
        
    }

    /// <summary>
    /// Method to add an item to a slot
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="slot">Slot to modify</param>
    public void AddItemToSlot(Item item, InventorySlot slot)
    {
        //Checks which inventory the slot belongs to
        if(slot.transform.parent.gameObject == inv1UI)
        {
            //Debug.Log("Inv1 slots: " + inv1Slots.Length);
            //Debug.Log("Inv1: " + inv1.inventory.Length);
            //Debug.Log(inv1Slots);
            //Debug.Log(inv1.GetType());
            //Debug.Log(slot.slotItem.name);
            int pos = System.Array.IndexOf(inv1Slots, slot.gameObject);
            inv1.AddItem(pos, item, slot);            
        }
        else
        {
            //Debug.Log("Inv2 slots: " + inv2Slots.Length);
            //Debug.Log("Inv2: " + inv2.inventory.Length);
            //Debug.Log(inv2.GetType());
            //Debug.Log(slot.slotItem.name);
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
        if (slot.slotItem == null) return;

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
                inv1 = slot.Inventory;
                inv1UI = inv1.invObject;
                selSlot1 = slot;
                //inv1.InitializeInventory();
                InitializeSlots();
                ExchangeItems();
                return;
            }
            if (selSlot2 == null)
            {
                slot.isSelected = true;
                inv2 = slot.Inventory;
                inv2UI = inv2.invObject;
                selSlot2 = slot;
                //inv2.InitializeInventory();
                InitializeSlots();
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
                inv1 = null;
                selSlot1 = null;
                return;
            }
            if (selSlot2 == slot)
            {
                inv2 = null;
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
            Item item1, item2;

            if (selSlot1.transform.parent.gameObject == inv1UI)
            {
                item1 = selSlot2.slotItem;
                item2 = selSlot1.slotItem;
            }
            else
            {
                item1 = selSlot1.slotItem;
                item2 = selSlot2.slotItem;
            }

            //Algo falla aca
            if (inv1.CorrectTypeItem(item1) && inv2.CorrectTypeItem(item2))
            {
                AddItemToSlot(selSlot2.slotItem, selSlot1);
                AddItemToSlot(temp, selSlot2);
            }


            selSlot1.isSelected = selSlot2.isSelected = false;
            selSlot1 = selSlot2 = null;
        }
    }
}

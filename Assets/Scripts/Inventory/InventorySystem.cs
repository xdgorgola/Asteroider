using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem invSyst;

    /// <summary>
    /// Inventario involucrado 1
    /// </summary>
    public Inventory inv1;
    /// <summary>
    /// Inventario involucrado 2
    /// </summary>
    public Inventory inv2;

    /// <summary>
    /// Slots de un inventario seleccionado.
    /// </summary>
    public InventorySlot selSlot1;
    public InventorySlot selSlot2;

    /// <summary>
    /// GameObjects asociados a las UI de los inventarios. Inventory1/Inventory2
    /// </summary>
    [SerializeField]
    private GameObject inv1UI;
    [SerializeField]
    private GameObject inv2UI;

    /// <summary>
    /// GameObjects asociados a los slots de los inventarios.
    /// </summary>
    private GameObject[] inv1Slots;
    private GameObject[] inv2Slots;

    private void Awake()
    {
        invSyst = this;   
    }

    //private void Start()
    //{
    //    InitializeSlots(true);    
    //}

    public void StartInvInteraction(Inventory inv11, Inventory inv22 = null)
    {
        //Initializing first inventory objects.
        inv1 = inv11;
        inv1UI = inv11.invObject;
        inv1UI.SetActive(true);
        inv1.InitializeInventory();

        //Initializing second inventory objects
        if (inv22 != null)
        {            
            inv2 = inv22;
            inv2UI = inv22.invObject;
            inv2UI.SetActive(true);            
            inv2.InitializeInventory();            
        }
        //Initializing slots
        InitializeSlots();
    }

    public void StopInvInteraction()
    {
        inv1 = null;
        inv1UI.SetActive(false);
        if (inv2 != null)
        {
            inv2 = null;
            inv2UI.SetActive(false);
        }
        //inv1UI.SetActive(false);
        //inv2UI.SetActive(false);
        //inv2 = null;
    }

    /// <summary>
    /// Method to add an item to a slot
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="slot">Slot to modify</param>
    public void AddItemToSlot(Item item, InventorySlot slot)
    {
        slot.AddItemToSlot(item);
        //Checks which inventory the slot belongs to
        if(slot.transform.parent.gameObject == inv1UI)
        {
            int pos = System.Array.IndexOf(inv1Slots, slot.gameObject);
            inv1.inventory[pos] = item;
            
        }
        else
        {
            int pos = System.Array.IndexOf(inv2Slots, slot.gameObject);
            inv2.inventory[pos] = item;
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
            inv1.inventory[pos] = null;
        }
        else
        {
            int pos = System.Array.IndexOf(inv2Slots, slot.gameObject);
            inv2.inventory[pos] = null;
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
        if(selSlot1 != null && selSlot2 != null)
        {
            Item temp = selSlot1.slotItem;
            AddItemToSlot(selSlot2.slotItem, selSlot1);
            AddItemToSlot(temp, selSlot2);

            selSlot1.isSelected = selSlot2.isSelected = false;
            selSlot1 = selSlot2 = null;

        }
    }
}

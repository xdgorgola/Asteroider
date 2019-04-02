using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }

    private bool isOpen = false;

    /// <summary>
    /// Maximun size of the inventory. Is given by the number of sltos in invObject.
    /// </summary>
    private int maxInvSize;
    /// <summary>
    /// Size of the inventory
    /// </summary>
    public int invSize;

    /// <summary>
    /// Inventory
    /// </summary>
    [SerializeField]
    public Item[] inventory;
    //Not inventory parent but inventory
    /// <summary>
    /// GameObject parent to the slots
    /// </summary>
    public GameObject invObject;

    /// <summary>
    /// Slots of the inventoru
    /// </summary>
    public GameObject[] inventorySlots;
    //public GameObject attachedUI;

    private void Start()
    {
        maxInvSize = invObject.transform.childCount;
        invObject.SetActive(false);
    }

    /// <summary>
    /// Initializes the slots items and the array that contains the slots.
    /// </summary>
    public void InitializeInventory()
    {
        //Initializes array that will contain the inventory slots, is it really needed?
        //Seems like yes
        inventorySlots = new GameObject[maxInvSize];

        //Iterating trough each slot GameObject and assigning them the items
        int i = 0;
        foreach (Transform child in invObject.transform)
        {
            inventorySlots[i] = child.gameObject;
            InventorySlot slot = child.GetComponent<InventorySlot>();
            if (i < invSize)
            {
                slot.slotItem = inventory[i];
            }
            else
            {
                slot.slotItem = null;
                child.gameObject.SetActive(false);
            }
            slot.UpdateSlot();
            i += 1;            
        }
    }

    /// <summary>
    /// Add item to the inventory
    /// </summary>
    /// <param name="itemPos">Index to add the item</param>
    /// <param name="item">Item to add</param>
    public virtual void AddItem(int itemPos, Item item)
    {
        inventory[itemPos] = item;
    }

    /// <summary>
    /// Remove item from inventory
    /// </summary>
    /// <param name="itemPos">Index to clear</param>
    public virtual void RemoveItem(int itemPos)
    {
        inventory[itemPos] = null;
    }

}

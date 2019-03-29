using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxInvSize;
    public int invSize;

    [SerializeField]
    public Item[] inventory;
    //Not inventory parent but inventory
    public GameObject invObject;

    public GameObject[] inventorySlots;
    //public GameObject attachedUI;

    private void Start()
    {
        invObject.SetActive(false);    
    }

    public void InitializeInventory()
    {
        inventorySlots = new GameObject[maxInvSize];
        int i = 0;
        Debug.Log(invObject.transform.childCount + " child count");
        Debug.Log(inventory.Length + " inv size");
        foreach(Transform child in invObject.transform)
        {
            Debug.Log(i);
            inventorySlots[i] = child.gameObject;
            InventorySlot slot = child.GetComponent<InventorySlot>();
            if (i < invSize)
            {
                slot.slotItem = inventory[i];
            }
            else
            {
                slot.slotItem = null;
            }
            slot.UpdateSlot();
            if(i >= invSize)
            {
                child.gameObject.SetActive(false);
            }
            i += 1;            
        }
    }

}

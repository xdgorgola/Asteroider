using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxInvSize;
    public int invSize;

    public Item[] inventory;
    public Item[] initialInv;
    //Not inventory parent but inventory
    public GameObject invObject;

    public GameObject[] inventorySlots;

    private void Start()
    {
        if (CompareTag("Player"))
        {
            invObject.SetActive(true);
            InitializeInventory();
        }
        invObject.SetActive(false);
    }

    public void InitializeInventory()
    {
        inventory = initialInv;
        inventorySlots = new GameObject[maxInvSize];
        int i = 0;
        foreach(Transform child in invObject.transform)
        {
            inventorySlots[i] = child.gameObject;
            InventorySlot slot = child.GetComponent<InventorySlot>();
            slot.slotItem = inventory[i];
            //Debug.Log(slot.slotItem.name);
            slot.UpdateSlot();
            //Debug.Log("i: " + i + " invSize: " + invSize);
            if(i >= invSize)
            {
                child.gameObject.SetActive(false);
            }
            i++;
            
            
            //inventorySlots[i] = child.gameObject;
            //inventory[i] = child.gameObject.GetComponent<InventorySlot>().slotItem;
            //i++;
        }
    }

}

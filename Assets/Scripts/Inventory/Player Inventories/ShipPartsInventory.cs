using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartsInventory : Inventory
{
    public override void AddItem(int itemPos, Item item, InventorySlot slot)
    {
        if (item == null)
        {
            inventory[itemPos] = item;
            slot.AddItemToSlot(item);
            return;
        }

        if (item.GetType() == typeof(ShipPart))
        {
            inventory[itemPos] = item;
            slot.AddItemToSlot(item);
            return;
        }
        Debug.Log("Item de tipo incorrecto.");
    }
}

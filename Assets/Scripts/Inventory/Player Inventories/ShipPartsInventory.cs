using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartsInventory : Inventory
{
    public ShipStatsEvents onPartChange = new ShipStatsEvents();

    public override void AddItem(int itemPos, Item item, InventorySlot slot)
    {
        Debug.Log(itemPos);
        inventory[itemPos] = item;
        slot.AddItemToSlot(item);
        onPartChange.Invoke();
    }

    public override void RemoveItem(int itemPos)
    {
        base.RemoveItem(itemPos);
        onPartChange.Invoke();
    }

    public override bool CorrectTypeItem(Item itemToCheck)
    {
        if (itemToCheck == null)
        {
            return true;
        }
        if (itemToCheck.GetType() == typeof(ShipPart))
        {
            return true;
        }
        return false;
    }
}

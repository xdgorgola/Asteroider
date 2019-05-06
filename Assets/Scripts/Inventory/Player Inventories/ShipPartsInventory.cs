using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartsInventory : Inventory
{
    /// <summary>
    /// Raised when a part is un/equipped
    /// </summary>
    public ShipStatsEvents onPartChange = new ShipStatsEvents();

    public override void AddItem(int itemPos, Item item, InventorySlot slot)
    {
        base.AddItem(itemPos, item, slot);
        onPartChange.Invoke();
    }

    public override void RemoveItem(int itemPos)
    {
        base.RemoveItem(itemPos);
        onPartChange.Invoke();
    }

    /// <summary>
    /// Checks if the item can be added to the inventory
    /// </summary>
    /// <param name="itemToCheck">Item to check if can be added</param>
    /// <returns></returns>
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

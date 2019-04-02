using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventories : Inventory
{
    //Script made just to override AddItem and RemoveItem methods in parent class Inventory
    //because it is needed to update the player/enemy weapon if there is any change in their
    //weapons inventory

    /// <summary>
    /// Same as parent class but now updates the equipped weapon.
    /// </summary>
    /// <param name="itemPos">Index for weapon insertion</param>
    /// <param name="item">Item to add</param>
    public override void AddItem(int itemPos, Item item)
    {
        inventory[itemPos] = item;
        UpdateWeapon();
    }

    /// <summary>
    /// Same as parent class but now updates the equipped weapon.
    /// </summary>
    /// <param name="itemPos">Index for weapon removal</param>
    public override void RemoveItem(int itemPos)
    {
        inventory[itemPos] = null;
        UpdateWeapon();
    }

    /// <summary>
    /// Updates the player equipped weapon in case of a inventory change
    /// </summary>
    /// <param name="weapon">Weapon to equip</param>
    public void UpdateWeapon()
    {
        if (CompareTag("Player"))
        {
            Debug.Log("Updating!");
            PlayerCombatHandler handler = GetComponent<PlayerCombatHandler>();
            handler.WeaponEquipped = (Weapon)inventory[handler.actualWeapon];
        }
        //Make enemies etc handler case
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventories : Inventory
{
    //Script made just to override AddItem and RemoveItem methods in parent class Inventory
    //because it is needed to update the player/enemy weapon if there is any change in their
    //weapons inventory

    protected override void Start()
    {
        base.Start();
        onInventoryChange.AddListener(UpdateWeapon);
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

    public override bool CorrectTypeItem(Item itemToCheck)
    {
        if (itemToCheck == null)
        {
            return true;
        }
        if (itemToCheck.GetType() == typeof(Weapon))
        {
            return true;
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public bool isSelected = false;

    public Item slotItem;
    [HideInInspector]
    public Image slotImage;

    private void Awake()
    {
        slotImage = transform.Find("Item Button").transform.Find("Item Icon").GetComponent<Image>();
        slotImage.sprite = (slotItem != null) ? slotItem.iImage: null;
    }

    /// <summary>
    /// Funcion para agregar un item al slot
    /// </summary>
    /// <param name="item">Item a agregar</param>
    public void AddItemToSlot(Item item)
    {
        slotItem = item;
        UpdateSlot();
    }

    /// <summary>
    /// Funcion para vaciar un slot
    /// </summary>
    public void RemoveItemFromSlot()
    {
        slotItem = null;
        UpdateSlot();
    }

    /// <summary>
    /// Funcion para actualizar la imagen de un slot
    /// </summary>
    public void UpdateSlot()
    {
        if (slotItem != null)
        {
            slotImage.sprite = slotItem.iImage;
        }
        else
        {
            slotImage.sprite = null;
        }
    }
}

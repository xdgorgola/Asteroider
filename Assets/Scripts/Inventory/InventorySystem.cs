using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem invSyst;

    /// <summary>
    /// Inventario del jugador.
    /// </summary>
    public Inventory playerInv;
    /// <summary>
    /// Otro inventario involucrado.
    /// </summary>
    public Inventory otherInv;

    /// <summary>
    /// Slots de un inventario seleccionado.
    /// </summary>
    public InventorySlot selSlot1;
    public InventorySlot selSlot2;

    /// <summary>
    /// GameObjects asociados a las UI de los inventarios. InventoryPlayer/Others
    /// </summary>
    [SerializeField]
    private GameObject playerInvUI;
    [SerializeField]
    private GameObject otherInvUI;

    /// <summary>
    /// GameObjects asociados a los slots de los inventarios.
    /// </summary>
    private GameObject[] playerSlots;
    private GameObject[] otherSlots;

    private void Awake()
    {
        invSyst = this;   
    }

    private void Start()
    {
        InitializeSlots(true);    
    }

    public void StartInvInteraction(Inventory other)
    {
        otherInv = other;
        playerInvUI.SetActive(true);
        otherInvUI.SetActive(true);
        otherInv.InitializeInventory();
        InitializeSlots();
    }

    public void StopInvInteraction()
    {
        playerInvUI.SetActive(false);
        otherInvUI.SetActive(false);
        otherInv = null;
    }

    /// <summary>
    /// Funcion para agregar un item a un slot
    /// </summary>
    /// <param name="item">Item a agregar</param>
    /// <param name="slot">Slot a modificar</param>
    public void AddItemToSlot(Item item, InventorySlot slot)
    {
        slot.AddItemToSlot(item);
        //Chequea si el slot es child de la UI del jugador
        if(slot.transform.parent.gameObject == playerInvUI)
        {
            int pos = System.Array.IndexOf(playerSlots, slot.gameObject);
            playerInv.inventory[pos] = item;
            
        }
        else
        {
            int pos = System.Array.IndexOf(otherSlots, slot.gameObject);
            otherInv.inventory[pos] = item;
        }

    }

    /// <summary>
    /// Funcion para vaciar un slot
    /// </summary>
    /// <param name="slot">Slot a vaciar</param>
    public void RemoveItemFromSlot(InventorySlot slot)
    {
        slot.RemoveItemFromSlot();
        //Chequea si el slot es child de la UI del jugador

        if (slot.transform.parent.gameObject == playerInvUI)
        {
            int pos = System.Array.IndexOf(playerSlots, slot.gameObject);
            playerInv.inventory[pos] = null;
        }
        else
        {
            int pos = System.Array.IndexOf(otherSlots, slot.gameObject);
            otherInv.inventory[pos] = null;
        }
    }

    /// <summary>
    /// Funcion para seleccionar un slot
    /// </summary>
    /// <param name="slotG">Slot a seleccionar</param>
    public void SelectSlot(GameObject slotG)
    {
        Debug.Log("Seleccionando slot");
        InventorySlot slot = slotG.GetComponent<InventorySlot>();
        //Si el slot no esta seleccionado, ahora si
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
        //Si el slot esta seleccionado, ahora no
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
            Debug.Log("EPA ACA PASA ALGO");
        }
    }

    /// <summary>
    /// Funcion para iniciar los arrays que contienen los GameObjects asociados a los slots
    /// </summary>
    /// <param name="firstTime">Es la primera vez que se inicializan los slots?</param>
    public void InitializeSlots(bool firstTime = false)
    {
        //Inicializar los slots del player
        if (firstTime)
        {
            playerSlots = playerInv.inventorySlots;
        }

        //Inicializar slots del otro inventario
        else
        {
            otherSlots = otherInv.inventorySlots;
        }
    }

    /// <summary>
    /// Funcion para intercambiar items entre dos slots
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

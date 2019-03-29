using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InventoryInteractionRevamped : MonoBehaviour
{
    [SerializeField]
    private int actualInv = 0;
    [SerializeField]
    private bool interacting = false;

    //private Inventory otherInv;
    [SerializeField]
    private List<Inventory> inventories;

    // Start is called before the first frame update
    void Start()
    {
        inventories = new List<Inventory>();
    }

    private void Update()
    {
        if (inventories.Count == 0)
        {
            actualInv = 0;
        }
        if (PlayerInput.InteractionDown && inventories.Count > 0 && !interacting)
        {
            interacting = true;
            InventorySystem.invSyst.StartInvInteraction(inventories[actualInv]);
        }

        else if (PlayerInput.InteractionDown && inventories.Count > 0 && interacting)
        {
            interacting = false;
            InventorySystem.invSyst.StopInvInteraction();
            if (actualInv < inventories.Count - 1)
            {
                actualInv += 1;
            }
            else
            {
                actualInv = 0;
            }
        }
    }
    
    public void RemoveInventory(Inventory toRemove)
    {
        inventories.Remove(toRemove);
        if (inventories.Count != 0)
        {
            if (actualInv > inventories.Count - 1)
            {
                actualInv = inventories.Count - 1;
            }
        }
        else
        {
            actualInv = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Test_Interaction"))
        {
            inventories.Add(collision.gameObject.GetComponentInParent<Inventory>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Test_Interaction"))
        {
            RemoveInventory(collision.gameObject.GetComponentInParent<Inventory>());
        }
    }

}

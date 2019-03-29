using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI invUI;

    private void Awake()
    {
        invUI = this;    
    }

    public void ActivateInvUI(GameObject firstInv, GameObject secondInv = null)
    {
        firstInv.SetActive(true);
        if (secondInv != null)
        {
            secondInv.SetActive(true);
        }
    }
   
}

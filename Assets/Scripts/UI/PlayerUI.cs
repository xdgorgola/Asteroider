using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text hpText;
    public Text shieldText;

    private void Awake()
    {
        GetComponent<HealthManager>().onLifeChange.AddListener(UpdateHP);
        GetComponent<HealthManager>().onShieldChange.AddListener(UpdateShield);
    }

    void UpdateHP()
    {
        hpText.text = "HP: "+ GetComponent<HealthManager>().Health.ToString("F2");
    }

    void UpdateShield()
    {
        Debug.Log(GetComponent<HealthManager>().Shield);
        shieldText.text = "Shield: " + GetComponent<HealthManager>().Shield.ToString("F2");
    }
}

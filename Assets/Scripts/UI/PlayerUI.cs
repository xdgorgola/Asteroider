using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text hpText;

    private void Awake()
    {
        GetComponent<HealthManager>().onLifeChange.AddListener(UpdateHP);
    }

    void UpdateHP()
    {
        hpText.text = "HP: "+ GetComponent<HealthManager>().Health.ToString();
    }
}

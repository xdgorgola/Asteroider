using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public float health = 100;

    public ShipsBase baseShip;
    //Una lista con las partes

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            AddHealth(10f);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ReduceHealth(10f);
        }
    }

    public void AddHealth(float add)
    {
        if (health + add >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += add;
        }

        //Hacer chequeo de si es el jugador y cambia la UI
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<PlayerUI>().hpText.text = "HP: " + health;
        }
    }

    public void ReduceHealth(float reduce)
    {
        if (health - reduce <= 0)
        {
            health = 0;
        }
        else
        {
            health -= reduce;
        }
       
        //Hacer chequeo de si es el jugador y cambia la UI
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<PlayerUI>().hpText.text = "HP: " + health;
        }

    }

}

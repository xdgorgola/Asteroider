using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeEvents : UnityEvent { }
public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public float Health
    {
        get { return health; }
    }
    private float health = 100;

    public LifeEvents onLifeChange = new LifeEvents();

    //Una lista con las partes

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            AddHealth(10f);
        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
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
        onLifeChange.Invoke();
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
        onLifeChange.Invoke();

    }

}

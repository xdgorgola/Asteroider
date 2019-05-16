using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour
{
    private int poolSize = 30;
    private bool isGrowable = false;

    [SerializeField]
    private GameObject asteroid;

    private List<GameObject> pool = new List<GameObject>();

    public static AsteroidPool asteroidPool;

    private void Awake()
    {
        asteroidPool = this;
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(asteroid);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetFromPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = pool[i];
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        if (isGrowable)
        {
            GameObject obj = (GameObject)Instantiate(asteroid);
            obj.SetActive(false);
            pool.Add(obj);
            poolSize += 1;
            return obj;
        }
        return null;
    }
}

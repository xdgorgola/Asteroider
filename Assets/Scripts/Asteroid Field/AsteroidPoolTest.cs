using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPoolTest : MonoBehaviour
{
    public enum AsteroidSize {Big, Medium, Small};

    [SerializeField]
    private int poolSize = 30;
    [SerializeField]
    private bool isGrowable = true;

    private List<GameObject> bigPool, mediumPool, smallPool;

    [SerializeField]
    private GameObject bigAsteroid;
    [SerializeField]
    private GameObject mediumAsteroid;
    [SerializeField]
    private GameObject smallAsteroid;

    void Start()
    {
        bigPool = new List<GameObject>();
        mediumPool = new List<GameObject>();
        smallPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(bigAsteroid);
            bigPool.Add(obj);
            obj.SetActive(false);
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(mediumAsteroid);
            mediumPool.Add(obj);
            obj.SetActive(false);
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(smallAsteroid);
            smallPool.Add(obj);
            obj.SetActive(false);
        }      
    }

    public GameObject GetAsteroidFromPool(AsteroidPoolTest.AsteroidSize size)
    {
        if (size == AsteroidPoolTest.AsteroidSize.Big)
        {
            foreach (GameObject asteroid in bigPool)
            {
                if (!asteroid.activeInHierarchy)
                {
                    asteroid.SetActive(true);
                    return asteroid;
                }
            }
        }
        else if (size == AsteroidPoolTest.AsteroidSize.Medium)
        {
            foreach (GameObject asteroid in mediumPool)
            {
                if (!asteroid.activeInHierarchy)
                {
                    asteroid.SetActive(true);
                    return asteroid;
                }
            }
        }
        else if (size == AsteroidPoolTest.AsteroidSize.Small)
        {
            foreach (GameObject asteroid in smallPool)
            {
                if (!asteroid.activeInHierarchy)
                {
                    asteroid.SetActive(true);
                    return asteroid;
                }
            }
        }

        if (isGrowable)
        {
            if (size == AsteroidPoolTest.AsteroidSize.Big)
            {
                GameObject obj = (GameObject)Instantiate(bigAsteroid);
                bigPool.Add(obj);
                obj.SetActive(true);
                return obj;
            }
            else if (size == AsteroidPoolTest.AsteroidSize.Medium)
            {
                GameObject obj = (GameObject)Instantiate(mediumAsteroid);
                mediumPool.Add(obj);
                obj.SetActive(true);
                return obj;
            }
            else if (size == AsteroidPoolTest.AsteroidSize.Small)
            {
                GameObject obj = (GameObject)Instantiate(smallAsteroid);
                smallPool.Add(obj);
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }
}

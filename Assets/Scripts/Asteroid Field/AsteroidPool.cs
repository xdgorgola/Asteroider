using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour
{
    public enum AsteroidSize {Big, Medium, Small};
    [SerializeField]
    private int poolSize = 30;
    [SerializeField]
    private bool isGrowable = false;

    [SerializeField]
    private GameObject bigAsteroid;
    [SerializeField]
    private GameObject mediumAsteroid;
    [SerializeField]
    private GameObject smallAsteroid;

    private Dictionary<AsteroidSize, List<GameObject>> pools = new Dictionary<AsteroidSize, List<GameObject>>();
    private List<GameObject> testPool = new List<GameObject>();
    public static AsteroidPool asteroidPool;

    void Awake()
    {
        Debug.Log("Inicializando pool");
        asteroidPool = this;
        pools[AsteroidSize.Big] = new List<GameObject>();
        pools[AsteroidSize.Medium] = new List<GameObject>();
        pools[AsteroidSize.Small] = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            Debug.Log(i);
            GameObject obj = (GameObject)Instantiate(bigAsteroid);
            pools[AsteroidSize.Big].Add(bigAsteroid);
            obj.SetActive(false);
        }
        for (int i = 0; i < poolSize; i++)
        {
            Debug.Log(i);
            GameObject obj = (GameObject)Instantiate(mediumAsteroid);
            pools[AsteroidSize.Medium].Add(mediumAsteroid);
            obj.SetActive(false);
        }
        for (int i = 0; i < poolSize; i++)
        {
            Debug.Log(i);
            GameObject obj = (GameObject)Instantiate(smallAsteroid);
            pools[AsteroidSize.Small].Add(smallAsteroid);
            obj.SetActive(false);
        }
        Debug.Log("Final Inicializando pool");
    }

    public GameObject GetFromPool(AsteroidSize size)
    {
        //List<GameObject> pool = pools[size];
        //for (int i = 0; i < poolSize; i++)
        //{
        //    if (!pool[i].activeInHierarchy)
        //    {
        //        GameObject obj = pool[i];
        //        Debug.Log(obj);
        //        obj.SetActive(true);
        //        return obj;
        //    }
        //}

            for (int i = 0; i < poolSize; i++)
            {
            Debug.Log("get" + i);
            Debug.Log(pools[size][i]);
                if (!pools[size][i].activeInHierarchy)
                {
                    GameObject obj = pools[size][i];
                    obj.SetActive(true);
                    Debug.Log(obj);
                    return obj;
                }
            }

        //if (isGrowable)
        //{
        //    if (size == AsteroidSize.Big)
        //    {
        //        GameObject obj = (GameObject)Instantiate(bigAsteroid);
        //        pool.Add(obj);
        //        obj.SetActive(true);
        //        return obj;
        //    }
        //    else if (size == AsteroidSize.Medium)
        //    {
        //        GameObject obj = (GameObject)Instantiate(mediumAsteroid);
        //        pool.Add(obj);
        //        obj.SetActive(true);
        //        return obj;
        //    }
        //    else if (size == AsteroidSize.Small)
        //    {
        //        GameObject obj = (GameObject)Instantiate(smallAsteroid);
        //        pool.Add(obj);
        //        obj.SetActive(true);
        //        return obj;
        //    }            
        //}
        return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilePool : MonoBehaviour
{
    public static PlayerProjectilePool playerPPool;

    public bool growable = true;
    public int poolSize = 35;
    public GameObject projectile;
    public List<GameObject> projectPool;

    private void Awake()
    {
        playerPPool = this;    
    }

    void Start()
    {
        //Intializing pool
        projectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(projectile);
            obj.SetActive(false);
            projectPool.Add(obj);
        }

    }

    public GameObject GetFromPool()
    {
        foreach(GameObject obj in projectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        if(growable)
        {
            GameObject obj = Instantiate(projectile);
            obj.SetActive(true);
            projectPool.Add(obj);
            return obj;
        }
        return null;
    }

}

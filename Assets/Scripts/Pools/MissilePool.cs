using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
    [SerializeField]
    private float poolSize = 10f;
    [SerializeField]
    private bool isGrowable = false;

    [SerializeField]
    private GameObject missileBase;
    private List<GameObject> pool = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (pool == null)
        {
            pool = new List<GameObject>();
        }    
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(missileBase);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetMissileFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        if (isGrowable)
        {
            GameObject obj = Instantiate(missileBase);
            obj.SetActive(true);
            pool.Add(obj);
            return obj;
        }
        return null;
    }
}

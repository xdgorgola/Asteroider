using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidParticlePools : MonoBehaviour
{
    [SerializeField]
    private int maxParticleSystem = 10;

    [SerializeField]
    private GameObject particles;

    private List<GameObject> pool = new List<GameObject>();

    private void Start()
    {
        for (int i =0; i < maxParticleSystem; i++)
        {
            GameObject obj = (GameObject)Instantiate(particles);
            pool.Add(obj);
            obj.SetActive(false);
        }
    }

    public GameObject GetAsteroidParticles()
    {
        foreach (GameObject particles in pool)
        {
            if (!particles.activeInHierarchy)
            {
                return particles;
            }
        }
        return null;
    }
}

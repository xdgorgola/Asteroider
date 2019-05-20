using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMisisle : MonoBehaviour
{
    public GameObject misilito;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("ACTIVADO EL MISILITO");
            misilito.SetActive(true);
            misilito.GetComponent<MissileMover>().SpawnMissile(transform, misilito.transform, 30f);
        }   
    }
}

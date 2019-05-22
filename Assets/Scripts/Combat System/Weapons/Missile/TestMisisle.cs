using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMisisle : MonoBehaviour
{
    public GameObject misilito;

    public bool drawDebug = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("ACTIVADO EL MISILITO");
            misilito.SetActive(true);
            //misilito.GetComponent<MissileMover>().Explode(10f);
        }   
    }

    private void OnDrawGizmos()
    {
        if (drawDebug)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(misilito.transform.position, 10f);
        }
    }
}

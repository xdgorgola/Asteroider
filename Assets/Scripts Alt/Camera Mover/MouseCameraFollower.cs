using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraFollower : MonoBehaviour
{
    Camera camera;
    Vector3 mousePos = Vector3.zero;
    Vector3 offset = new Vector3(0f, 0f, 0f);
    public Transform playerPos;

    [SerializeField]
    private int separationRange = 7;
    [SerializeField]
    private int closenessRange = 4;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        float mouseDistance = ( (mousePos - playerPos.position) + (Vector3.forward * 10) ).magnitude;
        Debug.Log((mousePos - playerPos.position + (Vector3.forward * 10)).magnitude);
        if (mouseDistance < separationRange && mouseDistance > closenessRange)
        {
            transform.position = mousePos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerPos.position, closenessRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerPos.position, separationRange);
    }
}

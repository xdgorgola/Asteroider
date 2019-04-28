using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MouseCameraFollower : MonoBehaviour
{
    /// <summary>
    /// Main camera
    /// </summary>
    private Camera cam;
    /// <summary>
    /// Mouse worldposition
    /// </summary>
    private Vector3 mousePos = Vector3.zero;
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 mouseOffset = Vector3.zero;
    /// <summary>
    /// target transform
    /// </summary>
    [SerializeField]
    private Transform target;

    /// <summary>
    /// Camera movement speed
    /// </summary>
    [SerializeField]
    private float cameraSpeed = 2f;

    [SerializeField]
    [Range(0,1)]
    private float smoothAmout = 0.125f;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        cam = Camera.main;
        //Debug.Log(Input.mousePosition);
    }

    private void LateUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = (mousePos - target.position).normalized;
        float distance;
        if ((mousePos - target.position).magnitude < 4)
        {
            distance = 0;
        }
        else
        {
            distance = Mathf.Clamp((mousePos - target.position).magnitude, 0f, 10f);
        }
        
        Vector3 desiredPosition = target.position + offset + (direction * distance) + mouseOffset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothAmout);
        transform.position = smoothedPosition;
    }
}

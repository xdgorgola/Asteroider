using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandarShipMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;

    //Test values to make a slider
    [SerializeField]
    private float rotationDamp = 7f;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    public void Update()
    {
        Rotate(camera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Debug.DrawLine(transform.position, camera.ScreenToWorldPoint(Input.mousePosition), Color.red, 1f);
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.right.normalized * movementSpeed * Time.deltaTime, Space.World);
        }
    }

    public void Rotate(Vector2 lookDirection)
    {
        Quaternion actualRotation = transform.rotation;
        Quaternion desiredRotation = Quaternion.LookRotation(transform.forward, Vector2.Perpendicular(lookDirection));
        Quaternion finalRotation = Quaternion.RotateTowards(actualRotation, desiredRotation, rotationDamp);
        transform.rotation = finalRotation;
    }
}

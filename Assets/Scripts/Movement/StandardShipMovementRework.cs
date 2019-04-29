using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardShipMovementRework : MonoBehaviour
{

    private Camera cam;
    [SerializeField]
    private float rotationDamp;

    //[SerializeField]
    //private float decelerationSpeed = 4f;

    private Rigidbody2D rb2d;
    private ShipStats ship;

    Vector2 lastDirection = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        rb2d = GetComponent<Rigidbody2D>();
        ship = GetComponent<ShipStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        Rotate(direction);
        if (Input.GetKey(KeyCode.W))
        {
            rb2d.velocity = new Vector3(direction.x, direction.y, 0) * ship.ShipSpeed;
            lastDirection = direction.normalized;
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

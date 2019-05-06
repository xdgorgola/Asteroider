using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardShipMovementRework : MonoBehaviour
{

    private Camera cam;
    [SerializeField]
    private float rotationDamp;

    private float speed = 1f;
    [SerializeField]
    private float decelerationSpeed = 4f;

    private Rigidbody2D rb2d;
    private ShipStats ship;
    //private ShipPartsInventory shipParts;

    Vector2 lastDirection = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        rb2d = GetComponent<Rigidbody2D>();
        ship = GetComponent<ShipStats>();
        //shipParts = GetComponent<ShipPartsInventory>();

        //shipParts.onPartChange.AddListener(UpdateMovement);
        ship.onPartsChange.AddListener(UpdateMovement);

    }

    private void Start()
    {
        rb2d.drag = decelerationSpeed;
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        speed = ship.ShipSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        Rotate(direction);
        if (Input.GetKey(KeyCode.W))
        {
            rb2d.velocity = new Vector3(direction.x, direction.y, 0) * speed; //*Time.deltaTime;
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

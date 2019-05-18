using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(ShipStats))]
public class StandardShipMovement : MonoBehaviour
{
    /// <summary> Ship rotation speed </summary>
    [SerializeField]
    private float rotationDamp;

    /// <summary> Ship speed </summary>
    [SerializeField]
    private float speed = 30f;
    /// <summary> Ship slow speed </summary>
    [SerializeField]
    private float slowSpeed = 15f;
    /// <summary> Ship deceleration speed </summary>
    [SerializeField]
    private float decelerationSpeed = 1f;

    /// <summary> Ship RigidBody2D </summary>
    private Rigidbody2D rb2d;
    /// <summary> Ship Stats </summary>
    private ShipStats ship;
    //private ShipPartsInventory shipParts;

    /// <summary> Variable for smoothDamp</summary>
    private float velocity = 0f;
    /// <summary> Actual ship speed </summary>
    protected float actualSpeed = 0f;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ship = GetComponent<ShipStats>();
        ship.onPartsChange.AddListener(UpdateMovement);
    }

    private void Start()
    {
        rb2d.drag = decelerationSpeed;
        UpdateMovement();
    }

    /// <summary> Updates ship speed stat </summary>
    public void UpdateMovement()
    {
        speed = ship.ShipSpeed;
    }

    /// <summary> Moves the ship forward (his front) </summary>
    protected void MoveForward()
    {
        Vector2 direction = (Vector2)transform.right.normalized;
        //Smooth transition between actual speed and desired one
        float finalSpeed = Mathf.SmoothDamp(actualSpeed, speed, ref velocity, 0.5f);
        actualSpeed = finalSpeed;
        rb2d.velocity = new Vector3(direction.x, direction.y, 0) * actualSpeed; //*Time.deltaTime;
    }

    /// <summary> Moves the ship forward but slower (his front) </summary>
    protected void MoveForwardSlower()
    {
        Vector2 direction = (Vector2)transform.right.normalized;
        //Smooth transition between actual speed and desired one
        float finalSpeed = Mathf.SmoothDamp(actualSpeed, slowSpeed, ref velocity, 0.5f);
        actualSpeed = finalSpeed;
        rb2d.velocity = new Vector3(direction.x, direction.y, 0) * actualSpeed; //*Time.deltaTime;
    }

    /// <summary> Moves the ship backward (his front) </summary>
    protected void MoveBackward()
    {
        Vector2 direction = ((Vector2)transform.right).normalized;
        //Smooth transition between actual speed and desired one
        float finalSpeed = Mathf.SmoothDamp(actualSpeed, -slowSpeed, ref velocity, 0.5f);
        actualSpeed = finalSpeed;
        rb2d.velocity = new Vector3(direction.x, direction.y, 0) * actualSpeed; //*Time.deltaTime;
    }

    /// <summary> Rotates the ship to certain direction </summary>
    /// <param name="lookDirection"> Direction to rotate to </param>
    protected void Rotate(Vector2 lookDirection)
    {
        Quaternion actualRotation = transform.rotation;
        Quaternion desiredRotation = Quaternion.LookRotation(transform.forward, Vector2.Perpendicular(lookDirection));
        Quaternion finalRotation = Quaternion.RotateTowards(actualRotation, desiredRotation, rotationDamp);
        transform.rotation = finalRotation;
    }

}

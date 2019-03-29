using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 15f;
    public float rotationSpeed = 6f;
    public float decelarationSpeed = 0.1f;

    PlayerShip pl;

    // Start is called before the first frame update
    void Awake()
    {
        pl = new PlayerShip();
        pl.forwardSpeed = forwardSpeed;
        pl.rotationSpeed = rotationSpeed;
        pl.decelarationSpeed = decelarationSpeed;

        pl.player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(pl.rotationSpeed);
        transform.Rotate(Vector3.forward, pl.rotationSpeed * (-PlayerInput.SteerInput));//pl.Rotation(Input.GetAxisRaw("Horizontal") * Time.deltaTime), );
        transform.Translate(pl.MoveForward(PlayerInput.VelocityInput) * Time.deltaTime, Space.Self);
    }
}

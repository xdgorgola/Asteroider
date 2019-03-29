using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip
{
    public float forwardSpeed = 15f;
    public float rotationSpeed = 6f;
    public float decelarationSpeed = 0.1f;

    public Vector2 velocity = Vector2.zero;

    public GameObject player;

    public Vector2 MoveForward(float inputSpeed)
    {
        if (inputSpeed != 0)
        {
            velocity = new Vector2(inputSpeed * forwardSpeed, velocity.y);
            return velocity;
        }
        else 
        {
            if (velocity.x <= decelarationSpeed)
            {
                velocity = new Vector2(0, velocity.y);
                return velocity;
            }
            else
            {
                //Debug.Log("Desacelerando");
                velocity = new Vector2(velocity.x - decelarationSpeed, velocity.y);
                return velocity;
            }
        }
    }

    public Vector3 Rotation(float inputRotation)
    {
        return new Vector3(0, 0, rotationSpeed * (-inputRotation));
    }

}

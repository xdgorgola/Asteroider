using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : StandardShipMovement
{

    private Camera cam;

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    private void Update()
    {
        //Pasar a player input!
        bool slow = Input.GetKey(KeyCode.LeftShift);
        Vector2 direction = ((cam.ScreenToWorldPoint(Input.mousePosition) - transform.position) - Vector3.forward * cam.transform.position.z).normalized;
        Rotate(direction);
        if (PlayerInput.VelocityInput > 0f && !slow)
        {
            MoveForward();
            return;
        }
        
        else if (PlayerInput.VelocityInput > 0f && slow)
        {
            Debug.Log("SLOWEST BITCH IN THE WEST");
            MoveForwardSlower();
            return;
        }
        
        else if (PlayerInput.VelocityInput < 0f)
        {
            MoveBackward();
            return;
        }
        //Importante.
        //Mejor todo el sistema para que haya inercia realmente
        actualSpeed = Mathf.SmoothDamp(actualSpeed, 1, ref velocity, 0.2f);
    }
}

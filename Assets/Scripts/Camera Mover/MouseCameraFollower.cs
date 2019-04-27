using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraFollower : MonoBehaviour
{
    /// <summary>
    /// Main camera
    /// </summary>
    private Camera camera;
    /// <summary>
    /// Mouse worldposition
    /// </summary>
    private Vector3 mousePos = Vector3.zero;
    private Vector3 offset = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Player transform
    /// </summary>
    [SerializeField]
    private Transform playerPos;

    /// <summary>
    /// Player to camera inner radius.
    /// </summary>
    [SerializeField]
    private float pcInnerRadius = 5f;
    /// <summary>
    /// Player to camera exterior radius
    /// </summary>
    [SerializeField]
    private float pcExteriorRadius = 6f;
    /// <summary>
    /// Camera to mouse inner radius
    /// </summary>
    [SerializeField]
    private float cmInnerRadius = 7f;
    /// <summary>
    /// Camera to mouse exterior radius
    /// </summary>
    [SerializeField]
    private float cmExteriorRadius = 8f;
    /// <summary>
    /// Camera movement speed
    /// </summary>
    [SerializeField]
    private float cameraSpeed = 2f;

    public GameObject testSphere;
    

    //NOTES
    //Some variables are not used, check them and delete them

        //No hace lo que quiero, necesito que la camara este en la direccion del mouse.

    private void Awake()
    {
        camera = Camera.main;
        Debug.Log(Input.mousePosition);
    }

    private void Update()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        //Distance betwheen the mouse and the camera
        float mouseDistance = ((mousePos - transform.position)).magnitude;
        
        Vector3 direction = ((mousePos - transform.position)).normalized;


        Vector3 directionMouse = (mousePos - playerPos.position + (Vector3.forward * 10)).normalized;
        Vector3 desiredPosition = playerPos.position + directionMouse * (pcExteriorRadius) - (Vector3.forward * 10);
        testSphere.transform.position = desiredPosition + (Vector3.forward * 10);

        //Debug.Log("Mouse pos: " + mousePos + " Camera pos: " + transform.position);
        //Debug.Log("Player pos: " + playerPos.position + " directionMouse: " + directionMouse);

        direction = (desiredPosition - transform.position).normalized;
        //Debug.Log(direction);
        //Distance between the camera and the player
        float cameraDistance = (transform.position - playerPos.position + (Vector3.forward * 10)).magnitude;
        //Debug.Log("Desired: " + desiredPosition);
        //Debug.Log(transform.position - playerPos.position + (Vector3.forward * 10));
        //Checks if the camera is in the allowed movement range and if the mouse is past the camera inner radius to make it move
        if (mouseDistance > cmInnerRadius) //&& cameraDistance < pcExteriorRadius)
        {
            Vector3 finalVector;
            float newCameraDistance = (transform.position + (direction * cameraSpeed) + Vector3.forward * 10).magnitude;
            //Debug.Log(transform.position + (direction * cameraSpeed) + Vector3.forward * 10);
            if (newCameraDistance > pcExteriorRadius - cameraDistance)
            {
                Debug.Log("No se puede");
                //finalVector = direction * (pcExteriorRadius - cameraDistance) * Time.deltaTime;
                transform.position = desiredPosition;
            }
            
            else if (newCameraDistance < pcExteriorRadius - cameraDistance)
            {
                Debug.Log("Se puede");
                finalVector = direction * cameraSpeed * Time.deltaTime;
                transform.Translate(finalVector);
            }
            else
            {
                Debug.Log("Ya esta");
                
            }
            //Debug.Log(finalVector);
            
        }
        else
        {
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(playerPos.position, pcInnerRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerPos.position, pcExteriorRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * 10, cmInnerRadius);
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(transform.position + Vector3.forward * 10, cmExteriorRadius);
    }
}

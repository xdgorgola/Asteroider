using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerRotator : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float rotationSpeed = 100f;

    [SerializeField]
    [Range(0, 0.05f)]
    private float smoothAmount = 0f;

    [SerializeField]
    private Vector3 offset = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothAmount);
        transform.RotateAround(target.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

}
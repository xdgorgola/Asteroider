using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipStats))]
public class StateControllerShip : StateController
{
    public List<Transform> wayPointList;
    [HideInInspector] public Transform wayPoint;
    [HideInInspector] public int wayPointIndex;

    public float stoppingDistance = 3f;
    public float shipDetectionDistance = 20f;
    public float loseRange = 30f;
    public float chaseRadius = 25f;
    [HideInInspector] public Transform chaseTarget;

    private ShipStats shipStats;
    [HideInInspector] public StandardShipMovement shipMover;
    public bool debugGizmos = true;

    private void Awake()
    {
        shipStats = GetComponent<ShipStats>();
        shipMover = GetComponent<StandardShipMovement>();
    }

    private void Start()
    {
        NextWaypoint();
    }

    public void NextWaypoint()
    {
        if (wayPointIndex + 1 >= wayPointList.Count) wayPointIndex = 0;
        else wayPointIndex += 1;
        wayPoint = wayPointList[wayPointIndex];
    }

    private void OnDrawGizmos()
    {
        if (debugGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, shipDetectionDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, loseRange);

        }
    }
}
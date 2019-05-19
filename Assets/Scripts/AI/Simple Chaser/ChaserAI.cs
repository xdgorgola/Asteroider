using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAI : MonoBehaviour
{
    [SerializeField]
    private float detectionRange = 20f;
    [SerializeField]
    private float patrolRadius = 20f;

    private Vector2 destination;
    private bool reachedDestination = true;

    private Transform player;
    private ChaserStates currentState = ChaserStates.Patrol;

    private float endChaseCounter = 0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case ChaserStates.Patrol:
                {
                    Debug.Log("Patrol state running");
                    if (PlayerInRange())
                    {
                        currentState = ChaserStates.Chase;
                        return;
                    }
                    if (reachedDestination)
                    {
                        reachedDestination = false;
                        destination = GenerateDestination();
                    }
                    else
                    {
                        if (InDestination())
                        {
                            reachedDestination = true;
                            destination = GenerateDestination();
                        }
                        else
                        {
                            Vector2 direction = (destination - (Vector2)transform.position).normalized;
                            transform.Translate(direction * 4f * Time.deltaTime, Space.World);
                            return;
                        }
                    }
                    Debug.Log("Patrol state repeat");
                    break;
                }

            case ChaserStates.Chase:
                {
                    Debug.Log("Chase state running");
                    if (PlayerInRange())
                    {
                        Debug.Log("Player in range, chasing it");
                        Vector2 direction = (player.position - transform.position).normalized;
                        transform.Translate(direction * 4f * Time.deltaTime, Space.World);
                        return;
                    }
                    else
                    {
                        Debug.Log("Chase state exit to endchase");
                        currentState = ChaserStates.EndChase;
                        return;
                    }
                }

            case ChaserStates.EndChase:
                {
                    Debug.Log("End chase state running");
                    endChaseCounter += Time.deltaTime;
                    if (PlayerInRange())
                    {
                        Debug.Log("End chase state exit to chase");
                        endChaseCounter = 0;
                        currentState = ChaserStates.Chase;
                        return;
                    }

                    if (endChaseCounter >= 3)
                    {
                        Debug.Log("End chase state exit to patrol");
                        endChaseCounter = 0;
                        currentState = ChaserStates.Patrol;
                        return;
                    }
                    Debug.Log("End chase state repeat");

                    break;
                }
        }
    }

    bool PlayerInRange()
    {
        if((transform.position - player.position).magnitude <= detectionRange)
        {
            return true;
        }
        return false;
    }

    Vector2 GenerateDestination()
    {
        Vector2 destination = (Vector2)transform.position + Random.insideUnitCircle * patrolRadius;
        return destination;
    }

    bool InDestination()
    {
        if(((Vector2)transform.position - destination).magnitude <= 2)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }


    public enum ChaserStates
    {
        Patrol,
        Chase,
        EndChase
    };
}

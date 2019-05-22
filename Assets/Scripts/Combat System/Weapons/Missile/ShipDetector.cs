using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ShipDetector : MonoBehaviour
{
    //The range should be the missile range?
    //Maybe do the missile detection range fixed?
    [SerializeField]
    private float range = 20f;
    /// <summary> Who are you "Aiming" from the detected gameobjects </summary>
    [SerializeField]
    private int aimingAt = 0;
    [SerializeField]
    private bool isPlayer = true;
    [SerializeField]
    private string detectionTag = "Shootable";

    private CircleCollider2D coll;
    private List<Transform> inRange = new List<Transform>();

    public bool drawDebug = false;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.radius = range;
        coll.isTrigger = true;
    }

    public void NextTarget()
    {
        if (aimingAt + 1 < inRange.Count)
        {
            aimingAt += 1;
        }
        else
        {
            aimingAt = 0;
        }
    }

    public void PreviousTarget()
    {
        if (aimingAt - 1 >= 0)
        {
            aimingAt -= 1;
        }
        else
        {
            aimingAt = (inRange.Count == 0 ? 0 : inRange.Count - 1);
        }
    }

    public Transform GetTarget()
    {
        if (inRange.Count > 0)
        {
            return inRange[aimingAt];
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MultiTag>() == null) return;
        if (isPlayer)
        {
            if (collision.GetComponent<MultiTag>().HasTag(detectionTag))
            {
                inRange.Add(collision.transform);
                Debug.Log(collision.gameObject.name + " BIENVENDIO");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MultiTag>() == null) return;
        if (isPlayer)
        {
            if (collision.GetComponent<MultiTag>().HasTag(detectionTag) && (inRange.Contains(collision.transform)))
            {
                inRange.Remove(collision.transform);
                Debug.Log(collision.gameObject.name + " CHAITO");
                if (aimingAt >= inRange.Count && inRange.Count > 0)
                {
                    aimingAt -= 1;
                }
                else if (inRange.Count == 0)
                {
                    aimingAt = 0;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawDebug)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }

}

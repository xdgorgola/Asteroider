using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShipsAI/Decisions/InDetectionRange")]
public class InDetectionRangeDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return InDetectionRange(controller);
    }

    private bool InDetectionRange(StateController controller)
    {
        StateControllerShip controllerS = controller as StateControllerShip;
        Collider2D[] hits;
        hits = Physics2D.OverlapCircleAll(controllerS.transform.position, controllerS.shipDetectionDistance);
        Debug.Log(hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(i);
            if (hits[i].GetComponent<MultiTag>().HasTag("Player"))
            {
                controllerS.chaseTarget = hits[i].transform;
                return true;
            }
        }
        return false;
    }
}

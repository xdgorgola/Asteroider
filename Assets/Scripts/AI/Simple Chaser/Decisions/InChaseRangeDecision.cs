using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShipsAI/Decisions/InChaseRange")]
public class InChaseRangeDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return InChaseRange(controller);
    }

    private bool InChaseRange(StateController controller)
    {
        StateControllerShip controllerS = controller as StateControllerShip;
        float distance = (controllerS.chaseTarget.position - controllerS.transform.position).magnitude;
        if (distance <= controllerS.chaseRadius)
        {
            return true;
        }
        return false;
    }
}

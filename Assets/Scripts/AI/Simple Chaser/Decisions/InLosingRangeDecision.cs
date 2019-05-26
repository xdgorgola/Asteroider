using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShipsAI/Decisions/InLosingRange")]
public class InLosingRangeDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return InLosingRange(controller);
    }

    private bool InLosingRange(StateController controller)
    {
        StateControllerShip controllerS = controller as StateControllerShip;
        float distance = (controllerS.chaseTarget.position - controllerS.transform.position).magnitude;
        if (distance <= controllerS.loseRange)
        {
            return true;
        }
        return false;
    }
}
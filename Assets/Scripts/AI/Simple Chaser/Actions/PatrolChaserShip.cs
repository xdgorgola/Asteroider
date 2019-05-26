using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShipsAI/Actions/ChaserShip/Patrol")]
public class PatrolChaserShip : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        StateControllerShip controllerS = controller as StateControllerShip;
        Vector2 wpDirection = (controllerS.wayPoint.position - controller.transform.position).normalized;
        float distance = (controllerS.wayPoint.position - controller.transform.position).magnitude;
        if (distance > controllerS.stoppingDistance)
        { 
            controllerS.shipMover.Rotate(wpDirection);
            controllerS.shipMover.MoveForward();
        }
        else
        {
            controllerS.NextWaypoint();
            controllerS.shipMover.Rotate(wpDirection);
            controllerS.shipMover.MoveForward();
        }
    }
}

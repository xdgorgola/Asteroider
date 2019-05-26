using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShipsAI/Actions/ChaserShip/Chase")]
public class ChaseChaseAction : Action
{
    public override void Act(StateController controller)
    {
        ChaseTarget(controller as StateControllerShip);
    }

    private void ChaseTarget(StateControllerShip controller)
    {
        Vector2 direction = (controller.chaseTarget.position - controller.transform.position).normalized;
        float distance = (controller.chaseTarget.position - controller.transform.position).magnitude;
        controller.shipMover.Rotate(direction);
        if (distance > controller.stoppingDistance)
        {
            controller.shipMover.MoveForward();
        }
    }
}

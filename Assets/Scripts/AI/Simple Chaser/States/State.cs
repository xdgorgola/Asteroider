using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShipsAI/State")]
public class State : ScriptableObject
{
    public Action[] enterActions;
    public Action[] actions;
    public Action[] exitActions;
    public Transition[] transitions;
    public Color debugGizmoColor = Color.grey;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    public void DoEnterActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            enterActions[i].Act(controller);
        }
    }

    //Todos agarran statecontroller
    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    public void DoExitActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            exitActions[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSuceeded = transitions[i].decision.Decide(controller);
            if (decisionSuceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}

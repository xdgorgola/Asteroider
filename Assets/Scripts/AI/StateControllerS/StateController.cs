using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;

    [SerializeField]
    private bool aiActive = true;

    [HideInInspector] public float stateTime = 0f;

    void Update()
    {
        if (!aiActive) return;
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnStateExit();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTime += Time.deltaTime;
        return (duration <= stateTime);
    }

    private void OnStateExit()
    {
        stateTime = 0;
    }
}

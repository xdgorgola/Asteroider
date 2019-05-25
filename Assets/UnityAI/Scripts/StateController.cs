using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    private bool aiActive = true;

    private void Update()
    {
        if (!aiActive) return;
        currentState.UpdateState(this);
    }
}

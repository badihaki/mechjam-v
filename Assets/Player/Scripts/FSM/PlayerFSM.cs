using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    [field: SerializeField] public PlayerState currentState { get; private set; }

    public void InitializeStateMachine(PlayerState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(PlayerState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}

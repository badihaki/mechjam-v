using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState currentState;
    [SerializeField] private EnemySceneEnterState sceneEnterState;

    public void InitializeFSM(Enemy enemy)
    {
        SetUpStates(enemy);

        currentState = sceneEnterState;
        currentState.EnterState();
    }

    private void SetUpStates(Enemy enemy)
    {
        sceneEnterState = ScriptableObject.CreateInstance<EnemySceneEnterState>();
        sceneEnterState.InitState("enter", enemy, this);
    }

    private void Update()
    {
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }
}

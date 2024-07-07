using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState currentState;
    [field: SerializeField] public EnemySceneEnterState sceneEnterState { get; private set; }
    [field: SerializeField] public EnemyGameplayState gameplayState { get; private set; }

    public void InitializeFSM(Enemy enemy)
    {
        SetUpStates(enemy);

        currentState = sceneEnterState;
        currentState.EnterState();
    }

    private void SetUpStates(Enemy enemy)
    {
        if (sceneEnterState) sceneEnterState.InitState("enter", enemy, this);
        if (gameplayState) gameplayState.InitState("gameplay", enemy, this);
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

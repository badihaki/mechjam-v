using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("Current State"), SerializeField] private EnemyState currentState;
    [field: SerializeField, Header("List of States")] public EnemySceneEnterState sceneEnterState { get; private set; }
    [field: SerializeField] public EnemyGameplayState gameplayState { get; private set; }

    public void InitializeFSM(Enemy enemy)
    {
        SetUpStates(enemy);

        currentState = sceneEnterState;
        currentState.EnterState();
    }

    private void SetUpStates(Enemy enemy)
    {
        sceneEnterState = ScriptableObject.CreateInstance<EnemySceneEnterState>();
        if (sceneEnterState) sceneEnterState.InitState("enter", enemy, this);
        gameplayState = ScriptableObject.CreateInstance<EnemyGameplayState>();
        if (gameplayState) gameplayState.InitState("gameplay", enemy, this);
    }

    private void Update()
    {
        currentState?.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState?.PhysicsUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }

    public void AnimationEndTriggered() => currentState.AnimationEnd();
}

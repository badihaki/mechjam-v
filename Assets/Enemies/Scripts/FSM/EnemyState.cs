using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName ="Enemy_x_State",menuName ="Game/Enemy/Enemy State"), Serializable]
public class EnemyState : ScriptableObject
{
    [field: SerializeField] public string stateAnimationName { get; protected set; }
    protected Enemy entity;
    protected EnemyStateMachine stateMachine;

    protected float stateStartTime;
    protected bool isExitingState;

    public void InitState(string _animName, Enemy _enemy, EnemyStateMachine _fsm)
    {
        stateAnimationName = _animName;
        entity = _enemy;
        stateMachine = _fsm;
    }

    public virtual void EnterState()
    {
        // enemy animation change
        stateStartTime = Time.time;
        isExitingState = false;
    }

    public virtual void ExitState()
    {
        isExitingState = true;
        // end state anim
    }

    public virtual void LogicUpdate()
    {
        if(!isExitingState)
        {
            CheckTransitions();
        }
    }
    public virtual void PhysicsUpdate() { }

    public virtual void CheckTransitions() { }

    public virtual void AnimationEnd() { }

}

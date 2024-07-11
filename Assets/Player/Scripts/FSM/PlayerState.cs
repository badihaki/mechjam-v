using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerState
{
    protected Player player;
    protected PlayerFSM stateMachine;
    protected string animationBoolName;

    public PlayerState(Player _player, PlayerFSM _fsm, string _animName)
    {
        player = _player;
        stateMachine = _fsm;
        animationBoolName = _animName;
    }

    protected float stateStartTime;
    protected bool isExitingState;

    #region enter/exit
    public virtual void Enter()
    {
        stateStartTime = Time.time;
        isExitingState = false;
        player.AnimationController.SetBool(animationBoolName, true);
    }
    public virtual void Exit()
    {
        isExitingState = true;
        player.AnimationController.SetBool(animationBoolName, false);
    }
    #endregion

    #region logic updates
    public virtual void LogicUpdate()
    {
        CheckStateTransitions();
    }

    public virtual void PhysicsUpdate() { }
    #endregion

    #region checks and triggers
    protected virtual void CheckStateTransitions()
    {
        //
    }

    public virtual void AnimationTrigger() { }
    public virtual void AnimationVfxTrigger() { }
    public virtual void AnimationSfxTrigger() { }
    #endregion
}

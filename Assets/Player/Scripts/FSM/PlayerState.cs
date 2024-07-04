using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void Enter()
    {
        stateStartTime = Time.time;
        isExitingState = false;
    }
    public void Exit()
    {
        isExitingState = true;
    }
    #endregion

    #region logic updates
    public void LogicUpdate()
    {
        CheckStateTransitions();
    }

    #endregion

    #region checks and triggers
    protected void CheckStateTransitions()
    {
        throw new NotImplementedException();
    }

    public void AnimationTrigger() { }
    public void AnimationVfxTrigger() { }
    public void AnimationSfxTrigger() { }
    #endregion
}

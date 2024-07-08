using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField, Serializable]
public class GameState
{
	protected GMFiniteStateMachine gameStateManager;
	public string stateName { get; protected set; }


	public GameState(GMFiniteStateMachine fsm, string name)
	{
		gameStateManager = fsm;
		stateName = name;
	}

	protected float stateStartTime;
	protected bool isExitingState;

	public virtual void EnterState()
	{
		isExitingState = false;
		stateStartTime = Time.time;
		Debug.Log($"GM is Entering state {stateName}");
	}
	public virtual void ExitState()
	{
		isExitingState = true;
	}
	public virtual void UpdateGameLogic() { }
}

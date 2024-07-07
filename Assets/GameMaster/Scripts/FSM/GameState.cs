using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
	protected GMFiniteStateMachine gameStateManager;
	protected string stateName;


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
		Debug.Log($"Entering state {stateName}");
	}
	public virtual void ExitState()
	{
		isExitingState = true;
	}
	public virtual void UpdateGameLogic() { }
}

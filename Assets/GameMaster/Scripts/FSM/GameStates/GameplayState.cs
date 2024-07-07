using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameplayState : GameState
{
	public GameplayState(GMFiniteStateMachine fsm, string name) : base(fsm, name)
	{
	}

	private bool startArena = false;

	public override void EnterState()
	{
		base.EnterState();

		GameMaster.Entity.SpawnPlayersInGameWorld();
	}

	public override void UpdateGameLogic()
	{
		base.UpdateGameLogic();
/*
		if (Time.time >= stateStartTime + 0.5f && startArena == false)
		{
			Debug.Log("spawning players");
			startArena = true;
			GameMaster.Entity.SpawnPlayersInGameWorld();
		}
*/
	}
}

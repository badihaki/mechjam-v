using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameplayState : GameState
{
	public GameplayState(GMFiniteStateMachine fsm, string name) : base(fsm, name)
	{
	}

	private bool startGameplay = false;
	private float gameplayStartTimer;

	public override void EnterState()
	{
		base.EnterState();

		GameMaster.Entity.SpawnPlayersInGameWorld();
		GameMaster.Entity.gameplayManager.SetWaves(UnityEngine.Random.Range(2, 4));
        // GameMaster.Entity.gameplayManager.SetWaves(1);
		// Debug.Log("1 wave for debug");
		gameplayStartTimer = UnityEngine.Random.Range(1.0f, 3.0f);
		startGameplay = false;
	}

	public override void UpdateGameLogic()
	{
		base.UpdateGameLogic();

		if(!startGameplay)
		{
			gameplayStartTimer -= Time.deltaTime;
			if(gameplayStartTimer < 0.0f )
			{
				startGameplay = true;
				gameplayStartTimer = 0.0f;
				GameMaster.Entity.gameplayManager.StartNewWave();
			}
		}
	}
}

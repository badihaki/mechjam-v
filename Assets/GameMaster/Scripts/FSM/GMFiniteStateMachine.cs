using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMFiniteStateMachine : MonoBehaviour
{
    [field: SerializeField] public GameState currentGameState { get; private set; }
	[field: SerializeField] public string currentStateName { get; private set; }

    // states
    public GameStartState gameStartState { get; private set; }
    public GameplayState gameplayState { get; private set; }
    public DevState devState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeGameState(GameState gameState)
    {
        currentGameState.ExitState();
        currentGameState = gameState;
        currentStateName = currentGameState.stateName;
        currentGameState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        currentGameState?.UpdateGameLogic();
    }

	public void InitializeGameStateMachine()
	{
        // start state machine
        gameStartState = new GameStartState(this, "Start ");
        gameplayState = new GameplayState(this, "Gameplay");
        devState = new DevState(this, "Dev");

        currentGameState = gameStartState;
		currentStateName = currentGameState.stateName;
        currentGameState.EnterState();
	}
}

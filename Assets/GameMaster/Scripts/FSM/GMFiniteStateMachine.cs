using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMFiniteStateMachine : MonoBehaviour
{
    [field: SerializeField] public GameState currentGameState { get; private set; }

    // states
    public GameStartState gameStartState { get; private set; }
    public GameplayState gameplayState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeGameState(GameState gameState)
    {
        currentGameState.ExitState();
        currentGameState = gameState;
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

        currentGameState = gameStartState;
        currentGameState.EnterState();
	}
}

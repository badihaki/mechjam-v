using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Entity { get; private set; }
    [field: SerializeField] public Vector3 gravity { get; private set; } = Physics.gravity;
    [field: SerializeField] public List<PlayerContainer> players { get; private set; }
    [field: SerializeField] public List<Player> gameplayPlayerList = new List<Player>();
    [field: SerializeField] public bool twoPlayerMode { get; private set; } = false;
    public GMFiniteStateMachine stateMachine { get; private set; }
    [SerializeField] private bool dev = false;

    private void OnEnable()
    {
        if (Entity != this && Entity != null)
        {
            print("gm singleton detected");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (Entity == null)
        {
            Entity = this;
            InitializeGame();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            print("gm singleton detected");
            Destroy(gameObject);
        }
    }

    private void InitializeGame()
    {
        stateMachine = GetComponent<GMFiniteStateMachine>();
        stateMachine.InitializeGameStateMachine();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        PlayerContainer newPlayer = playerInput.GetComponent<PlayerContainer>();
        newPlayer.SetID(players.Count + 1);
        players.Add(newPlayer);
        if (!twoPlayerMode && players.Count == 1)
        {
            print("start game");
            stateMachine.ChangeGameState(stateMachine.gameplayState);
        }
    }

    public void SpawnPlayersInGameWorld()
    {
        /*
                foreach (PlayerContainer player in players)
                {
                    Transform spawn = GameObject.Find($"P{player.playerId}Spawn").transform;
                    Player playerPrefab = Instantiate(player.playerGameplayPrefab, spawn.position, Quaternion.identity).GetComponent<Player>();
                }
        */
        for (int i = 0; i < players.Count; i++)
        {
            print(i + 1);
            Transform spawn = GameObject.Find($"P{i + 1}Spawn").transform;
            print(spawn.name);
            GameObject playerPrefab = Instantiate(players[i].playerGameplayPrefab, spawn.position, Quaternion.identity);
		}
    }
}

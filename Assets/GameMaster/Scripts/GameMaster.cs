using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Entity { get; private set; }
    [field: SerializeField] public Vector3 gravity { get; private set; } = Physics.gravity;
    [field: SerializeField] public List<Player> playerList = new List<Player>();
    [field: SerializeField] public bool twoPlayerMode { get; private set; } = false;
    [field: SerializeField] public bool dev { get; private set; } = false;
    public GMFiniteStateMachine stateMachine { get; private set; }
    public GameplayManager gameplayManager { get; private set; }

    private void Awake()
    {
        if (Entity == null && Entity != this)
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

    }

    private void InitializeGame()
    {
        gameplayManager = GetComponent<GameplayManager>();
        stateMachine = GetComponent<GMFiniteStateMachine>();
        stateMachine.InitializeGameStateMachine();
    }

    // Update is called once per frame
    void Update()
    {

    }

/*
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Player player = playerInput.GetComponent<Player>();
        player.PlayerSetup();
    }
*/

    public void StartGame(bool twoPlayer)
    {
        twoPlayerMode = twoPlayer;
        ChangeGameScene(2);
    }

    public void StartDevRoom()
    {
        ChangeGameScene(1);
    }

    public void BeginGameplay(FloorManager newFloor)
    {
        gameplayManager.SetFloor(newFloor);
        if (dev) stateMachine.ChangeGameState(stateMachine.devState);
        else
        {
            stateMachine.ChangeGameState(stateMachine.gameplayState);
        }
    }

	public void ChangeGameScene(int sceneId)
	{
		SceneManager.LoadScene(sceneId);
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
        for (int i = 0; i < playerList.Count; i++)
        {
            // Player player = Instantiate(playerList[i], spawn.position, Quaternion.identity);
            print($"try to find spawn with id of {playerList[i].PlayerID}");
			Transform spawn = GameObject.Find($"P{playerList[i].PlayerID}Spawn").transform;
			Player player = playerList[i];
            player.transform.position = spawn.position;
            player.StartPlayerGameplay();
		}
    }
}

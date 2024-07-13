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
    [field: SerializeField] public bool gameStarted { get; private set; } = false;
    public GMFiniteStateMachine stateMachine { get; private set; }
    public GameplayManager gameplayManager { get; private set; }
    private Transform canvas;
    private Animator animationController;
    private WaitForSeconds endLoadWaitTime = new WaitForSeconds(2.57f);

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

    public void StartLoading()
    {
        canvas.gameObject.SetActive(true);
        stateMachine.ChangeGameState(stateMachine.loadingState);
        animationController.SetBool("loading", true);
    }

    public IEnumerator EndLoading()
    {
        animationController.SetBool("loading", false);
        yield return endLoadWaitTime;
        canvas.gameObject.SetActive(false);
        playerList.ForEach(player =>
        {
            player.ExitCinematic();
        });

        // gameplayManager.SetWaves(UnityEngine.Random.Range(2, 4));
        stateMachine.ChangeGameState(stateMachine.gameplayState);
    }

    private void InitializeGame()
    {
        gameplayManager = GetComponent<GameplayManager>();
        canvas = transform.Find("Canvas");
        canvas.gameObject.SetActive(false);
        animationController = GetComponent<Animator>();
        stateMachine = GetComponent<GMFiniteStateMachine>();
        stateMachine.InitializeGameStateMachine();
    }

    // Update is called once per frame
    void Update()
    {

    }

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
        if (dev)
        {
            stateMachine.ChangeGameState(stateMachine.devState);
        }
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
        if (!gameStarted)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                // Player player = Instantiate(playerList[i], spawn.position, Quaternion.identity);
                print($"try to find spawn with id of {playerList[i].PlayerID}");
			    Transform spawn = GameObject.Find($"P{playerList[i].PlayerID}Spawn").transform;
			    Player player = playerList[i];
                player.transform.position = spawn.position;
                player.StartPlayerGameplay();
		    }
            gameStarted = true;
        }
    }
}

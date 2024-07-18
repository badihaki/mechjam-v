using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Transform endGameScreen;
	private Transform winText;
    private Transform loseText;

	private Animator animationController;
    private WaitForSeconds endLoadWaitTime = new WaitForSeconds(1.57f);
    [field: SerializeField] public AudioSource audioController { get; private set; }

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
		GameMaster.Entity.playerList.ForEach(player =>
		{
			print("entering cinematic");
			player.EnterCinematic();
		});
		canvas.gameObject.SetActive(true);
        // if (!twoPlayerMode)
        //     canvas.transform.Find("P2Container").gameObject.SetActive(false);
        stateMachine.ChangeGameState(stateMachine.loadingState);
        animationController.SetBool("loading", true);
    }

    public IEnumerator EndLoading()
    {
        animationController.SetBool("loading", false);
        
        yield return endLoadWaitTime;
        
        canvas.gameObject.SetActive(false);
        
        stateMachine.ChangeGameState(stateMachine.gameplayState);

        playerList.ForEach(player =>
        {
            player.ReturnToGameplayState();
        });
    }

    private void InitializeGame()
    {
        gameplayManager = GetComponent<GameplayManager>();
        audioController = transform.AddComponent<AudioSource>();
        
        canvas = transform.Find("Canvas");
        endGameScreen = canvas.Find("EndGame");
        winText = endGameScreen.Find("Win").transform;
        loseText = endGameScreen.Find("Lose").transform;

        endGameScreen.gameObject.SetActive(false);
		canvas.gameObject.SetActive(false);

        animationController = GetComponent<Animator>();
        
        stateMachine = GetComponent<GMFiniteStateMachine>();
        stateMachine.InitializeGameStateMachine();
    }

    public void WinGame()
    {
        Time.timeScale = 0.0f;

        canvas.gameObject.SetActive(true);
		endGameScreen.gameObject.SetActive(true);

		loseText.gameObject.SetActive(false);

		playerList.ForEach(player =>
        {
            player.Deactivate();
        });
    }
    public void LoseGame()
    {
        Time.timeScale = 0.0f;

		canvas.gameObject.SetActive(true);
        endGameScreen.gameObject.SetActive(true);
		
		winText.gameObject.SetActive(false);

		playerList.ForEach(player =>
		{
			player.Deactivate();
		});
	}

	public void QuitGame()
	{
		// save any game data here
#if UNITY_EDITOR
		// Application.Quit() does not work in the editor so
		// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

    public void RestartGame(bool twoPlayer)
    {
        Time.timeScale = 1.0f;

        playerList.ForEach(player =>
        {
            player.Health.InitializeHealth(20);
            player.StartPlayerGameplay();
        });
        StartGame(twoPlayer);
    }

    public void StartGame(bool twoPlayer)
    {
        twoPlayerMode = twoPlayer;
        StartLoading();
        StartCoroutine(WaitForGameLoad());
    }

    private IEnumerator WaitForGameLoad()
    {
        yield return endLoadWaitTime;
        ChangeGameScene(2);
        StartCoroutine(EndLoading());
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
                // print($"try to find spawn with id of {playerList[i].PlayerID}");
			    Transform spawn = GameObject.Find($"P{playerList[i].PlayerID}Spawn").transform;
			    Player player = playerList[i];
                player.StartPlayerGameplay();
                player.transform.position = spawn.position;
		    }
            gameStarted = true;
        }
    }
}

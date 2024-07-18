using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private Button singleStartBtn;
    [SerializeField] private Button coopStartBtn;
    [SerializeField] private Button nextBtn;
    // Start is called before the first frame update
    void Start()
    {
        nextBtn = transform.Find("Title").Find("NxtBtn").GetComponent<Button>();
        nextBtn.gameObject.SetActive(false);
        // singleStartBtn = transform.Find("Start P1").GetComponent<Button>();
        // singleStartBtn.gameObject.SetActive(false);
        // coopStartBtn = transform.Find("Start P2").GetComponent<Button>();
        // coopStartBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(bool twoPlayers)
    {
        if (GameMaster.Entity.playerList.Count > 0) GameMaster.Entity.StartGame(twoPlayers);
    }
    public void StartDev()
    {
        GameMaster.Entity.StartDevRoom();
    }
	public void OnPlayerJoined(PlayerInput playerInput)
	{
        // print($"count {GameMaster.Entity.playerList.Count}");
        if(GameMaster.Entity.playerList.Count == 0)
        {
            nextBtn.gameObject.SetActive(true);
            // singleStartBtn.gameObject.SetActive(true);
        }
        else
        {
            // coopStartBtn.gameObject.SetActive(true);
        }
		Player player = playerInput.GetComponent<Player>();
		player.PlayerSetup();
        print($"player{player.PlayerID} is ready");
	}

    public void QuitGame() => GameMaster.Entity.QuitGame();
}

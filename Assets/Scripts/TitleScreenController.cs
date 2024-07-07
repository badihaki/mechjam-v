using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private Button singleStartBtn;
    [SerializeField] private Button coopStartBtn;
    [SerializeField] private Button devStartBtn;
    // Start is called before the first frame update
    void Start()
    {
        singleStartBtn = transform.Find("Start P1").GetComponent<Button>();
        singleStartBtn.gameObject.SetActive(false);
        coopStartBtn = transform.Find("Start P2").GetComponent<Button>();
        coopStartBtn.gameObject.SetActive(false);
        devStartBtn = transform.Find("Dev").GetComponent<Button>();
        devStartBtn.gameObject.SetActive(false);
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
        print($"count {GameMaster.Entity.playerList.Count}");
        if(GameMaster.Entity.playerList.Count == 0)
        {
            singleStartBtn.gameObject.SetActive(true);
            devStartBtn.gameObject.SetActive(true);
        }
        else
        {
            coopStartBtn.gameObject.SetActive(true);
        }
		Player player = playerInput.GetComponent<Player>();
		player.PlayerSetup();
        print($"player{player.PlayerID} is ready");
	}
}

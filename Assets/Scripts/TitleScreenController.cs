using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(bool twoPlayers)
    {
        GameMaster.Entity.StartGame(twoPlayers);
    }
	public void OnPlayerJoined(PlayerInput playerInput)
	{
		Player player = playerInput.GetComponent<Player>();
		player.PlayerSetup();
        print($"player{player.PlayerID} is ready");
	}
}

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

    private void OnEnable()
    {
        if(Entity != this && Entity!=null)
        {
            print("gm singleton detected");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(Entity == null)
        {
            Entity = this;
        }
        else
        {
            print("gm singleton detected");
            Destroy(gameObject);
        }
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
	}
}

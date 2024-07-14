using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextEnvironmentTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
/*
            GameMaster.Entity.playerList.ForEach(player =>
            {
                Console.WriteLine("entering cinematic");
                print("entering cinematic");
                player.EnterCinematic();
            });
*/
            Console.WriteLine("Going to next board");
            print("Going to next board");
            GameMaster.Entity.gameplayManager.currentFloor.GoToNextEnvironment();
        }
    }
}

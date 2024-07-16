using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public Canvas menu;
    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponentInChildren<Canvas>();
        menu.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        menu.gameObject.SetActive(true);

        GameMaster.Entity.playerList.ForEach(player =>
        {
            player.Deactivate();
        });

        Time.timeScale = 0.0f;
    }

    public void UnpauseGame()
    {
        menu.gameObject.SetActive(false);

        Time.timeScale = 1.0f;

        GameMaster.Entity.playerList.ForEach(player =>
        {
            player.Activate();
        });
    }
}

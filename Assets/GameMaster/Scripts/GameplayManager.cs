using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField, Header("Pause")] private GameObject pausePrefab;
    [field: SerializeField] public PauseManager pauseMenu { get; private set; }
    [field: SerializeField, Header("Floors")] public FloorManager currentFloor { get; private set; }
    [Header("Waves")]
    private WaitForSeconds waveWaitTime = new WaitForSeconds(3.0f);
    [field: SerializeField] public bool waveStarted { get; private set; } = false;
    [field: SerializeField] public int wavesLeft { get; private set; }
    [field: SerializeField] public int enemiesLeft { get; private set; }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakePauseMenu()
    {
        if (!pauseMenu)
            pauseMenu = Instantiate(pausePrefab, transform.position, Quaternion.identity).GetComponent<PauseManager>();
        else
            pauseMenu?.UnpauseGame();
    }

    public void StartNewWave()
    {
        if(wavesLeft > 0)
        {
            currentFloor.SpawnEnemies();
            wavesLeft--;
            waveStarted = true;
        }
    }
    public void SetWaves(int waveNum) => wavesLeft = waveNum;
    public void AddEnemy() => enemiesLeft++;
    public void KillEnemy()
    {
        enemiesLeft--;
        if (enemiesLeft <= 0)
        {
            waveStarted = false;
            if (wavesLeft > 0)
            {
                StartCoroutine(SetUpNewWave());
            }
            else
            {
                currentFloor.CompleteBoard();
            }
        }
    }

    private IEnumerator SetUpNewWave()
    {
        yield return waveWaitTime;
        StartNewWave();
    }

    public void SetFloor(FloorManager floor) => currentFloor = floor;
}

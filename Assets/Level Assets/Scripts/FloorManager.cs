using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class FloorManager : MonoBehaviour
{
    [field: SerializeField] public ENVManager startingEnvironment { get; private set; }
    [field: SerializeField] public ENVManager currentEnvironment { get; private set; }
    [field: SerializeField] public int numberOfEnvironments { get; private set; }
    [field: SerializeField] public List<ENVManager> nextPossibleEnvironments { get; private set; }
    private WaitForSeconds envLoadWait = new WaitForSeconds(1.25f);
    [field: SerializeField] public bool isAtShop { get; private set; }
    public ENVManager shopEnv;
    private bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!ready) BuildFloor();
    }

    public void BuildFloor()
    {
        currentEnvironment = startingEnvironment;
        GameMaster.Entity.BeginGameplay(this);
        SetUpCam();
        numberOfEnvironments = UnityEngine.Random.Range(3, 6);
        ready = true;
    }

    private static void SetUpCam()
    {
        CinemachineVirtualCamera vCam = GameObject.Find("VCam").GetComponent<CinemachineVirtualCamera>();
        vCam.Follow = GameMaster.Entity.playerList[0].transform;
        vCam.LookAt = GameMaster.Entity.playerList[0].transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemies()
    {
        int numberToSpawn = UnityEngine.Random.Range(1, currentEnvironment.enemySpawnPositions.Count - 1);
        for (int i = 0; i < numberToSpawn; i++)
        {
            EnemySpawn spawnPoint = GetEnemySpawn();
            Enemy enemyToSpawn = currentEnvironment.enemyList[UnityEngine.Random.Range(0, currentEnvironment.enemyList.Count - 1)];
            GameMaster.Entity.gameplayManager.AddEnemy();
            Instantiate(enemyToSpawn, spawnPoint.transform.position, Quaternion.identity);
            spawnPoint.OccupySpawn();
            // print($"spawning enemy #{i}/{numberToSpawn}::{enemyToSpawn.name} || at spawn point: {spawnPoint.name}");
        }
    }

    private EnemySpawn GetEnemySpawn()
    {
        List<EnemySpawn> spawnPoints = new List<EnemySpawn>();
        currentEnvironment.enemySpawnPositions.ForEach(spawn =>
        {
            if (!spawn.isOccupied) spawnPoints.Add(spawn);
        });
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count - 1)];
    }

    public void CompleteBoard()
    {
        if (numberOfEnvironments > 0)
        {
            numberOfEnvironments--;
            currentEnvironment.FinishBoard();
        }
    }

    public void GoToNextEnvironment()
    {
        GameMaster.Entity.StartLoading();
        
        int roll = UnityEngine.Random.Range(1, 6);
        print($"We rolled a {roll}");
        
        if (roll <= 4 || isAtShop) StartCoroutine(LoadNextEnv());
        else StartCoroutine(LoadShopEnv());
    }

    private IEnumerator LoadNextEnv()
    {
        isAtShop = false;

        int envIndex = UnityEngine.Random.Range(0, nextPossibleEnvironments.Count - 1);
        Vector3 envSpawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.x + 125.0f);
        
        yield return envLoadWait;
        
        Destroy(currentEnvironment.gameObject);
        // make new environment the current environment
        // currentEnvironment = Instantiate(nextPossibleEnvironments[envIndex].gameObject, envSpawnPos, Quaternion.identity).GetComponent<ENVManager>();
        currentEnvironment = Instantiate(nextPossibleEnvironments[envIndex].gameObject, transform.position, Quaternion.identity).GetComponent<ENVManager>();
        currentEnvironment.BuildEnv();
        // put player in starting zone for next environment
        GameMaster.Entity.playerList.ForEach(player =>
        {
            if (player.PlayerID == 1)
                player.transform.position = currentEnvironment.p1Start.position;
            else if (player.PlayerID == 2)
                player.transform.position = currentEnvironment.p2Start.position;
        });

        // complete fade
        StartCoroutine(TriggerEndLoad());
    }

    private IEnumerator LoadShopEnv()
    {
        isAtShop = true;

        Vector3 envSpawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.x + 125.0f);

        yield return envLoadWait;

        Destroy(currentEnvironment.gameObject);
        // make new environment the current environment
        currentEnvironment = Instantiate(shopEnv.gameObject, transform.position, Quaternion.identity).GetComponent<ENVManager>();
        currentEnvironment.BuildEnv();
        // put player in starting zone for next environment
        GameMaster.Entity.playerList.ForEach(player =>
        {
            if (player.PlayerID == 1)
                player.transform.position = currentEnvironment.p1Start.position;
            else if (player.PlayerID == 2)
                player.transform.position = currentEnvironment.p2Start.position;
        });

        // complete fade
        StartCoroutine(TriggerEndLoad());
    }

    private IEnumerator TriggerEndLoad()
    {
        yield return envLoadWait;
        StartCoroutine(GameMaster.Entity.EndLoading());
    }
}

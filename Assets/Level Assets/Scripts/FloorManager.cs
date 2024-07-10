using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [field: SerializeField] public ENVManager startingEnvironment { get; private set; }
    [field: SerializeField] public ENVManager currentEnvironment { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        currentEnvironment = startingEnvironment;
        GameMaster.Entity.BeginGameplay(this);
        SetUpCam();
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
        int numberToSpawn = UnityEngine.Random.Range(0, currentEnvironment.enemySpawnPositions.Count - 1);
        print($"spawning {numberToSpawn} enemies");
        for (int i = 0; i < numberToSpawn; i++)
        {
            EnemySpawn spawnPoint = GetEnemySpawn();
            Enemy enemyToSpawn = currentEnvironment.enemyList[UnityEngine.Random.Range(0, currentEnvironment.enemyList.Count - 1)];
            GameMaster.Entity.gameplayManager.AddEnemy();
            Instantiate(enemyToSpawn, spawnPoint.transform.position, Quaternion.identity);
            spawnPoint.OccupySpawn();
            print($"spawning enemy #{i}/{numberToSpawn}::{enemyToSpawn.name} || at spawn point: {spawnPoint.name}");
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
}

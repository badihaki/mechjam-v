using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public Health health { get; private set; }
    [field: SerializeField] public Transform target { get; private set; }
    public EnemyLocomotionController locomotionController { get; private set; }
    [field: SerializeField] public EnemyStateMachine stateMachine { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        InitializeEntity(new Vector2(5, 12));
    }

    private void InitializeEntity(Vector2 healthRange)
    {
        // health
        health = transform.AddComponent<Health>();
        int healthValue = (int)Mathf.Round(UnityEngine.Random.Range(healthRange.x, healthRange.y));
        health.InitializeHealth(healthValue);
        health.onHealthChange += DidEntityDie;

        // locomotion
        locomotionController = GetComponent<EnemyLocomotionController>();
        locomotionController.InitiializeController(this);

        BuildStateMachine();
        FindNewPlayerTarget();
    }

    private void BuildStateMachine()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        stateMachine?.InitializeFSM(this);
    }

    public void FindNewPlayerTarget()
    {
        print(GameMaster.Entity);
        if (GameMaster.Entity.dev)
        {
            target = GameObject.Find("Player").transform;
            return;
        }
        int playerIndex = (int)Mathf.Round(UnityEngine.Random.Range(0, GameMaster.Entity.playerList.Count - 1));
        print(playerIndex);
        target = GameMaster.Entity.playerList[playerIndex].transform;
    }

    private void OnEnable()
    {
        if (health) health.onHealthChange += DidEntityDie;
    }
    private void OnDisable()
    {
        health.onHealthChange -= DidEntityDie;
    }

    private void DidEntityDie(int health)
    {
        if(health <= 0)
        {
            print("entity died");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(Transform entity, int damage, Vector2 force)
    {
        health.ChangeHealth(health.currentHealth - damage);
    }
}

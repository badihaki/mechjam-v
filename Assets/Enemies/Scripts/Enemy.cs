using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public Health health { get; protected set; }
    [SerializeField] private Vector2 healthRanges;
    [field: SerializeField] public Transform target { get; protected set; }
    public EnemyLocomotionController locomotionController { get; protected set; }
    public EnemyAttackController attackController { get; protected set; }
    [field: SerializeField] public Animator animationController { get; private set; }
    protected EnemyInventory inventory;
    [field: SerializeField, Header("State Machine")] public EnemyStateMachine stateMachine { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        int healthMin = (int)MathF.Round(healthRanges.x);
        int healthMax = (int)MathF.Round(healthRanges.y);
        InitializeEntity(healthMin, healthMax);
    }

    protected virtual void InitializeEntity(int min, int max)
    {
        // health
        health = transform.AddComponent<Health>();
        int healthValue = UnityEngine.Random.Range(min, max);
        health.InitializeHealth(healthValue);
        health.onHealthChange += DidEntityDie;

        // items/inventory
        inventory = GetComponent<EnemyInventory>();

        // locomotion
        locomotionController = GetComponent<EnemyLocomotionController>();
        locomotionController.InitiializeController(this);

        // attack
        attackController = GetComponent<EnemyAttackController>();
        attackController.InitializeController(this);

        // animation
        animationController = transform.Find("Gfx").GetComponent<Animator>();

        // state machine
        BuildStateMachine();

        // find player
        FindNewPlayerTarget();
    }

    protected void BuildStateMachine()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        stateMachine?.InitializeFSM(this);
    }

    public void FindNewPlayerTarget()
    {
        int playerIndex = (int)Mathf.Round(UnityEngine.Random.Range(0, GameMaster.Entity.playerList.Count - 1));
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

    protected void DidEntityDie(int health)
    {
        if(health <= 0)
        {
            print("entity died");
            inventory.DropItem();
            GameMaster.Entity.gameplayManager.KillEnemy();
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
        animationController.SetTrigger("hurt");
    }
}

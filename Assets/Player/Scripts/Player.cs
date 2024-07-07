using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour, IDamageable
{
    // these are public so other scripts have access, but value can only be modified here
    [field: SerializeField] public int playerID { get; private set; }
    [field: SerializeField] public bool isUsingKBMP { get; private set; } = false;
    public PlayerControlsManager controls { get; private set; }
    [field: SerializeField] public Health health { get; private set; }
    public PlayerLocomotionController locomotionController { get; private set; }
    public PlayerAttackController attackController { get; private set; }
    public PlayerUIController uIController { get; private set; }

    // state machine below
    private PlayerFSM stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSetup();
        
        // controls
        controls = GetComponent<PlayerControlsManager>();

        // health
        health = transform.AddComponent<Health>();
        health.InitializeHealth(20);
        health.onHealthChange += DidEntityDie;
        
        // locomotion
        locomotionController = GetComponent<PlayerLocomotionController>();
        locomotionController.InitializeController(this);

        // attack
        attackController = GetComponent<PlayerAttackController>();
        attackController.InitializeController(this);

        // ui
        uIController = GetComponent<PlayerUIController>();
        uIController.InitializeController(this);

        // state machine
        stateMachine = GetComponent<PlayerFSM>();
        stateMachine.InitializeStateMachine(this);
    }

    private void PlayerSetup()
    {
        playerID = GameMaster.Entity.gameplayPlayerList.Count + 1;
        GameMaster.Entity.gameplayPlayerList.Add(this);
        // in full game, go ahead and have game master do this
        string controlType = GetComponent<PlayerInput>().currentControlScheme;
        print(controlType);
        if (controlType == "KBM") isUsingKBMP = true;
        else isUsingKBMP = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DidEntityDie(int hp)
    {
        if (hp <= 0) Die();
    }

    private void Die()
    {
        print($"player({playerID}) was killed");
        GameMaster.Entity.gameplayPlayerList.Remove(this);
        Destroy(gameObject);
    }

    public void Damage(Transform entity, int damage, Vector2 force)
    {
        print($"player({playerID}) was damaged by {entity.name}");
        health.ChangeHealth(damage);
    }
}

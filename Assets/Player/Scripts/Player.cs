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
	[field: SerializeField] public int PlayerID { get; private set; }
	[field: SerializeField] public bool IsUsingKBMP { get; private set; } = false;
    public PlayerControlsManager Controls { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public PlayerLocomotionController LocomotionController { get; private set; }
    [field: SerializeField] public PlayerAttackController AttackController { get; private set; }
	[field: SerializeField] public PlayerUIController UIController { get; private set; }
	[field: SerializeField] public GameObject CharacterModel { get; private set; }
    [field:SerializeField] public Animator AnimationController {  get; private set; }
    private WaitForSeconds hurtAnimTime = new WaitForSeconds(0.35f);

    private bool playerReady = false;
    
    // state machine below
    private PlayerFSM stateMachine;

    // dev mode
    [SerializeField] private bool devMode = false;

	private void Awake()
    {
        if (!playerReady)
        {
            // PlayerSetup();
            DontDestroyOnLoad(gameObject);
        
            // controls
            Controls = GetComponent<PlayerControlsManager>();

            // character model
            CharacterModel = transform.Find("Char").gameObject;
            CharacterModel.SetActive(false);

            // animation
            AnimationController = CharacterModel.GetComponent<Animator>();

            // health
            Health = transform.AddComponent<Health>();
            Health.InitializeHealth(20);
            Health.onHealthChange += DidEntityDie;
        
            // locomotion
            LocomotionController = GetComponent<PlayerLocomotionController>();
            LocomotionController.InitializeController(this);
            LocomotionController.physicsController.useGravity = false;
            LocomotionController.enabled = false;

            // attack
            AttackController = GetComponent<PlayerAttackController>();
            AttackController.InitializeController(this);
            AttackController.enabled = false;

		    // state machine
		    stateMachine = GetComponent<PlayerFSM>();
            stateMachine.InitializeStateMachine(this);

            playerReady = true;
        }
        // if (devMode || GameMaster.Entity.dev) StartDevMode();
    }

    public void PlayerSetup()
    {
        SetPID(GameMaster.Entity.playerList.Count + 1);
        gameObject.name = $"P-{PlayerID}";
        GameMaster.Entity.playerList.Add(this);
        SetControlType();
    }

    private void SetControlType()
    {
        string controlType = GetComponent<PlayerInput>().currentControlScheme;
        print($"controlType is {controlType}");
        if (controlType == "KBM") IsUsingKBMP = true;
        else IsUsingKBMP = false;
    }

    public void StartDevMode()
    {
        print("Player is starting gameplay in ~DEV MODE~");
        SetPID(1);
        SetControlType();
        StartPlayerGameplay();
    }

    public void EnterCinematic() => stateMachine.ChangeState(stateMachine.cinematicState);
    public void ExitCinematic() => stateMachine.ChangeState(stateMachine.gameplayState);

    public void StartPlayerGameplay()
    {
		LocomotionController.enabled = true;
		LocomotionController.physicsController.useGravity = true;
        LocomotionController.GetNewCamera();
        AttackController.enabled = true;

        // ui
		UIController = transform.AddComponent<PlayerUIController>();
		UIController.InitializeController(this);
        
        // character model
        CharacterModel.SetActive(true);
        stateMachine.ChangeState(stateMachine.gameplayState);

        // will need to build character later
    }

    public void SetPID(int id) => PlayerID = id;

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
        print($"player({PlayerID}) was killed");
        GameMaster.Entity.playerList.Remove(this);
        Destroy(gameObject);
    }

    public void Damage(Transform entity, int damage, Vector2 force)
    {
        print($"player({PlayerID}) was damaged by {entity.name}");
        Health.ChangeHealth(Health.currentHealth - damage);
        StartCoroutine(HurtEntity());
    }

    private IEnumerator HurtEntity()
    {
        stateMachine.ChangeState(stateMachine.hurtState);
        
        yield return hurtAnimTime;
        
        stateMachine.ChangeState(stateMachine.gameplayState);
    }
}

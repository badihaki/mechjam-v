using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // these are public so other scripts have access, but value can only be modified here
    [field: SerializeField] public int playerID { get; private set; }
    [field: SerializeField] public bool isUsingKBMP { get; private set; } = false;
    public PlayerControlsManager controls { get; private set; }
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
        playerID = GameMaster.Entity.playerList.Count + 1;
        GameMaster.Entity.playerList.Add(this);
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
}

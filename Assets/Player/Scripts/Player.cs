using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public int playerID { get; private set; }
    [field: SerializeField] public bool isUsingKBMP { get; private set; } = false;
    public PlayerControlsManager controls { get; private set; }
    public PlayerLocomotionController locomotionController { get; private set; }
    private PlayerFSM stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSetup();
        controls = GetComponent<PlayerControlsManager>();
        
        // locomotion
        locomotionController = GetComponent<PlayerLocomotionController>();
        locomotionController.InitializeController(this);

        stateMachine = GetComponent<PlayerFSM>();
        stateMachine.InitializeStateMachine(this);
    }

    private void PlayerSetup()
    {
        playerID = 0;
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

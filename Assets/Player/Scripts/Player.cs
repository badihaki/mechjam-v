using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public int playerID { get; private set; }
    [field: SerializeField] public PlayerControlsManager controls { get; private set; }
    [field: SerializeField] public PlayerLocomotionController locomotionController { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<PlayerControlsManager>();
        
        // locomotion
        locomotionController = GetComponent<PlayerLocomotionController>();
        locomotionController.InitializeController(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

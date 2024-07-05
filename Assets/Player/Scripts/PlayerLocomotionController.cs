using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionController : MonoBehaviour
{
    private Player player;
    private Rigidbody physicsController;

    private bool ready = false;
    
    public void InitializeController(Player _player)
    {
        player = _player;
        physicsController = GetComponent<Rigidbody>();
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            ControlMovement();
        }
    }

    private void ControlMovement()
    {
        Vector3 movement = new Vector3(player.controls.moveInput.x, 0.0f, player.controls.moveInput.y);

        if(movement != Vector3.zero )
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.25f);
        }

        physicsController.velocity = movement;
    }
}

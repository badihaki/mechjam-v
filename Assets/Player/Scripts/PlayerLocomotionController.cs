using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionController : MonoBehaviour
{
    private Player player;
    [SerializeField] private float speed = 5.5f;
    private Rigidbody physicsController;
    [SerializeField] private Vector3 rotationTarget;
    [SerializeField] private float rotationSpeed = 0.25f;

    private Camera cam;

    public void InitializeController(Player _player)
    {
        player = _player;
        physicsController = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ControlMovement()
    {
        if (player.isUsingKBMP)
        {
            // kbm
            KbmMove();
        }
        else
        {
            // gamepad
            GamepadMove();
        }
    }

    private void KbmMove()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(player.controls.lookInputKbm);

        if(Physics.Raycast(ray, out hit))
        {
            rotationTarget = hit.point;
        }
        MovePlayerWithAim();
    }

    private void GamepadMove()
    {
        if (player.controls.lookInputGamepad == Vector2.zero) MovePlayer();
        else MovePlayerWithAim();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(player.controls.moveInput.x, 0.0f, player.controls.moveInput.y);

        if(movement != Vector3.zero )
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        }

        physicsController.velocity = movement * speed;
    }

    private void MovePlayerWithAim()
    {
        if (player.isUsingKBMP)
        {
            Vector3 lookPosition = rotationTarget - transform.position;
            lookPosition.y = 0.0f;
            Quaternion rotation = Quaternion.LookRotation(lookPosition);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0.0f, rotationTarget.z);
            if(aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(player.controls.lookInputGamepad.x, 0.0f, player.controls.lookInputGamepad.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), rotationSpeed);
            }
        }

        Vector3 movement = new Vector3(player.controls.moveInput.x, 0.0f, player.controls.moveInput.y);
        physicsController.velocity = movement * speed;
    }
}

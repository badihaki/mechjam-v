using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionController : MonoBehaviour
{
    private Player player;
    [SerializeField] private float speed = 5.5f;
    [SerializeField] private float currentSpeed;
    public Rigidbody physicsController { get; private set; }
    [SerializeField] private Vector3 rotationTarget;
    [SerializeField] private float rotationSpeed = 0.25f;
    private WaitForSeconds dashRefreshTime; // how much time before you can dash again
    [SerializeField] private float timeToDash = 0.15f;
    [SerializeField] private float dashTimer = 0.0f;
    [SerializeField] private bool dashAvailable = true;
    [SerializeField] private float dashForce = 35.0f;
    [field: SerializeField] public bool movementEnabled { get; private set; }

    private Camera cam;

    public void InitializeController(Player _player)
    {
        player = _player;
        physicsController = GetComponent<Rigidbody>();
        movementEnabled = true;
        dashRefreshTime = new WaitForSeconds(1.50f);
    }

    public void GetNewCamera() => cam = Camera.main;

	// Update is called once per frame
	void Update()
    {
        player.AnimationController.SetFloat("speed", currentSpeed);
    }

    public void ControlMovement()
    {
        if (player.IsUsingKBMP)
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
        Ray ray = cam.ScreenPointToRay(player.Controls.lookInputKbm);

        if (Physics.Raycast(ray, out hit))
        {
            rotationTarget = hit.point;
        }
        MovePlayerWithAim();
    }

    private void GamepadMove()
    {
        if (player.Controls.lookInputGamepad == Vector2.zero) MovePlayer();
        else MovePlayerWithAim();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(player.Controls.moveInput.x, physicsController.velocity.y, player.Controls.moveInput.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        }
        currentSpeed = Mathf.Clamp(movement.magnitude, 0.0f, 1.0f);
        physicsController.velocity = movement * speed;
    }

    private void MovePlayerWithAim()
    {
        if (player.IsUsingKBMP)
        {
            Vector3 lookPosition = rotationTarget - transform.position;
            lookPosition.y = 0.0f;
            Quaternion rotation = Quaternion.LookRotation(lookPosition);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0.0f, rotationTarget.z);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(player.Controls.lookInputGamepad.x, 0.0f, player.Controls.lookInputGamepad.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), rotationSpeed);
            }
        }

        Vector3 movement = new Vector3(player.Controls.moveInput.x, 0.0f, player.Controls.moveInput.y);

        currentSpeed = Mathf.Clamp(movement.magnitude, 0.0f, 1.0f);

        physicsController.velocity = movement * speed;
    }

    public void CanBoostDash()
    {
        if (dashAvailable && player.Controls.dashInput)
        {
            StartCoroutine(BoostDash());
            StartCoroutine(ResetDash());
        }
    }

    /*
        private IEnumerator BoostDash()
        {
            print("boost dashing");
            dashAvailable = false;
            player.controls.UseDash();
            Vector3 movement = new Vector3(player.controls.moveInput.x, 0.0f, player.controls.moveInput.y);
            if (movement == Vector3.zero) movement = transform.forward;
            // physicsController.AddForce(movement * dashForce, ForceMode.Impulse);
            physicsController.velocity = movement * dashForce;
            yield return new WaitForSeconds(dashRefreshTime);
            physicsController.velocity = Vector3.zero;
            dashAvailable = true;
        }
    */
    private IEnumerator BoostDash()
    {
        // turn off ability to dash while we dashin'
        movementEnabled = false;
        dashTimer += timeToDash;
        
        // get direction of dash
        Vector3 movement = new Vector3(player.Controls.moveInput.x, 0.0f, player.Controls.moveInput.y);
        if (movement == Vector3.zero) movement = transform.forward;

        // apply direction and dashForce to physicsController's velocity
        while (dashTimer > 0.0f)
        {
            dashTimer -= Time.deltaTime;
            physicsController.velocity = movement * dashForce;
            yield return null;
        }

        // turn back on ability to dash
        physicsController.velocity = Vector3.zero;
        movementEnabled = true;
    }

    private IEnumerator ResetDash()
    {
        dashAvailable = false;
        yield return dashRefreshTime;
        dashAvailable = true;
    }
}

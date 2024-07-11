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
    private WaitForSeconds dashWait = new WaitForSeconds(0.15f);
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

    public void SetCanDash(bool canDash) => dashAvailable = canDash;

    public void CanDashBoost()
    {
        if (dashAvailable && player.Controls.dashInput)
        {
            player.AnimationController.SetBool("dash", true);
            StartCoroutine(ResetDashBool());
            StartCoroutine(DashBoost());
            StartCoroutine(ResetDash());
        }
    }

    public void ZeroOutVelocity() => physicsController.velocity = Vector3.zero;

    private IEnumerator ResetDashBool()
    {
        yield return dashWait;

        player.AnimationController.SetBool("dash", false);
    }
    private IEnumerator DashBoost()
    {
        // turn off ability to dash, move and shoot while we dashin'
        movementEnabled = false;
        dashTimer += timeToDash;
        player.AttackController.SetCanMelee(false);
        player.AttackController.SetCanShoot(false);

        // get direction of dash
        Vector3 movement = new Vector3(player.Controls.moveInput.x, 0.0f, player.Controls.moveInput.y);
        transform.LookAt(movement);
        if (movement == Vector3.zero) movement = transform.forward;

        // apply direction and dashForce to physicsController's velocity
        while (dashTimer > 0.0f)
        {
            dashTimer -= Time.deltaTime;
            physicsController.velocity = movement * dashForce;
            yield return null;
        }

        // turn back on ability to dash, melee and shoot
        player.AttackController.SetCanMelee(true);
        player.AttackController.SetCanShoot(true);
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

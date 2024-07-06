using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsManager : MonoBehaviour
{
    [field: SerializeField] public Vector2 moveInput { get; private set; }
    [field: SerializeField] public Vector2 lookInputKbm { get; private set; }
    [field: SerializeField] public Vector2 lookInputGamepad { get; private set; }
    [field: SerializeField] public bool shootInput { get; private set; }
    [field: SerializeField] public bool meleeInput { get; private set; }
    [field: SerializeField] public bool dashInput { get; private set; }
    [field: SerializeField] public bool reloadInput { get; private set; }

	#region move/look
	public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>().normalized;
        float x = Mathf.Round(input.x);
        float y = Mathf.Round(input.y);
        moveInput = new Vector2(x, y);
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        lookInputKbm = context.ReadValue<Vector2>();
    }
    public void OnGamepadLook(InputAction.CallbackContext context)
    {
        lookInputGamepad = context.ReadValue<Vector2>();
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed) dashInput = true;
        else if (context.canceled) dashInput = false;
    }
    public void UseDash() => dashInput = false;
    #endregion

    #region combat
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed) shootInput = true;
        else if (context.canceled) shootInput = false;
    }
    public void OnMelee(InputAction.CallbackContext context)
    {
        if (context.performed) meleeInput = true;
        else if (context.canceled) meleeInput = false;
    }
    public void UseMelee() => meleeInput = false;
    public void OnReload(InputAction.CallbackContext context)
    {
		if (context.performed) reloadInput = true;
		else if (context.canceled) reloadInput = false;
	}
    public void UseReload() => reloadInput = false;
	#endregion
}

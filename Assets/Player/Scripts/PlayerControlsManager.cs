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
}

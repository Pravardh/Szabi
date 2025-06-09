using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour, Player.ILocomotionActions
{
    private Player playerInput;

    public event Action OnPlayerMove;
    public event Action OnPlayerShoot;

    private void Awake()
    {
        playerInput = new Player();
        EnableInput();
        playerInput.Locomotion.AddCallbacks(this);

    }

    public void DisableInput()
    {
        playerInput.Disable();
    }

    public void EnableInput()
    {
        playerInput.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed) //You have pressed a button
            OnPlayerMove?.Invoke();

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPlayerShoot?.Invoke();
        }

    }
}

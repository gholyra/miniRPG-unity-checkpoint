using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;

    public event Action<bool> OnParry;
    public event Action<bool> OnRun;
    public event Action OnAttack;

    public Vector2 MoveDirection => 
        playerControls.Gameplay.Move.ReadValue<Vector2>();
    
    public InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Enable();
        playerControls.Gameplay.Parry.performed += OnParryPerformedAndCanceled;
        playerControls.Gameplay.Parry.canceled += OnParryPerformedAndCanceled;
        playerControls.Gameplay.Run.performed += OnRunPerformedAndCanceled;
        playerControls.Gameplay.Run.canceled += OnRunPerformedAndCanceled;
        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
    }   

    public void EnableGameplayInput()
    {
        playerControls.Gameplay.Enable();
    }

    public void DisableGameplayInput()
    {
        playerControls.Gameplay.Disable();
    }

    private void OnParryPerformedAndCanceled(InputAction.CallbackContext context)
    {
        OnParry?.Invoke(context.ReadValueAsButton());
    }

    private void OnRunPerformedAndCanceled(InputAction.CallbackContext context)
    {
        OnRun?.Invoke(context.ReadValueAsButton());
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }
}

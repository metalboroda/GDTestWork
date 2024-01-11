using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDTestWork
{
  public class InputManager : MonoBehaviour
  {
    public event Action AttackMainPressed;
    public event Action AttackSuperPressed;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
      _playerInputActions = new();
    }

    private void OnEnable()
    {
      _playerInputActions.OnFeet.AttackMain.performed += OnAttackMainButton;
      _playerInputActions.OnFeet.AttackSuper.performed += OnAttackSuperButton;
    }

    private void Start()
    {
      OnFeetChange(true);
    }

    private void OnDisable()
    {
      _playerInputActions.OnFeet.AttackMain.performed -= OnAttackMainButton;
      _playerInputActions.OnFeet.AttackSuper.performed -= OnAttackSuperButton;
    }

    public void OnFeetChange(bool change)
    {
      if (change == true)
      {
        _playerInputActions.OnFeet.Enable();
      }
      else
      {
        _playerInputActions.OnFeet.Disable();
      }
    }

    public Vector2 GetMovementAxis()
    {
      return _playerInputActions.OnFeet.Movement.ReadValue<Vector2>();
    }

    private void OnAttackMainButton(InputAction.CallbackContext obj)
    {
      AttackMainPressed?.Invoke();
    }

    private void OnAttackSuperButton(InputAction.CallbackContext obj)
    {
      AttackSuperPressed?.Invoke();
    }
  }
}
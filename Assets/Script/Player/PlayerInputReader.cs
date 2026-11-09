using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    public event Action Interacted;

    public Vector2 Move => _input.Player.Move.ReadValue<Vector2>();
    public Vector2 Look => _input.Player.Look.ReadValue<Vector2>();

    private SporelightInput _input;

    private void Awake()
    {
        _input = new SporelightInput();
        _input.Player.Interact.performed += _ => Interacted?.Invoke();
    }

    private void OnEnable() => _input.Player.Enable();
    private void OnDisable() => _input.Player.Disable();
}
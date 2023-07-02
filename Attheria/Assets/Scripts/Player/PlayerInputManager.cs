using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputManager : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public event EventHandler OnInputEnabled;

    private void Awake()
    {
        PlayerInput = new();
    }

    private void OnEnable()
    {
        PlayerInput.Enable();
        OnInputEnabled?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable() => PlayerInput.Disable();
}
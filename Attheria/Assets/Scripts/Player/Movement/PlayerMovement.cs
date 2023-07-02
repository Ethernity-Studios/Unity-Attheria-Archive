using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputManager _playerInputManager;
    
    private void Start()
    {
        _playerInputManager.OnInputEnabled += OnInputEnabled;
    }

    void OnInputEnabled(object sender, EventArgs e)
    {
        _playerInputManager.PlayerInput.PlayerMovement.Enable(); 
    }
}

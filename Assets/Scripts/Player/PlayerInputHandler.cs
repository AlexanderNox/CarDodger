using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInputMap _playerInputMap;
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInputMap = new PlayerInputMap();
    }

    private void Update()
    {
        _playerMovement.SetMoveVector( _playerInputMap.Player.Move.ReadValue<Vector2>());
        if (_playerInputMap.Player.Drift.IsPressed())
        {
            _playerMovement.ApplyDriftMode();
        }
    }
    
    private void OnEnable()
    {
        _playerInputMap.Enable();
    }

    private void OnDisable()
    {
        _playerInputMap.Disable();
    }
}

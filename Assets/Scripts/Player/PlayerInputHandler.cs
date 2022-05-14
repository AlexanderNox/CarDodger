using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    private InputController _inputController;
    private PlayerMovement _playerMovement;
    private void Awake()
    {
        _inputController = InputController.Instance;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _inputController.OnStartSteering += ctx => _playerMovement.SteeringInput = ctx;
        // _inputController.OnPerformedSteering += StopSteeringInput;
        _inputController.OnStartAcceleration += ctx => _playerMovement.AccelerationInput = ctx;
        // _inputController.OnPerformedAcceleration += StopAccelerationInput;
    }
    
    private void OnDisable()
    {
        _inputController.OnStartSteering -= ctx => _playerMovement.SteeringInput = ctx;
        // _inputController.OnPerformedSteering -= StopSteeringInput;
        _inputController.OnStartAcceleration -= ctx => _playerMovement.AccelerationInput = ctx;
        // _inputController.OnPerformedSteering -= StopAccelerationInput;
    }

    // private void StopSteeringInput()
    // {
    //     _playerMovement.SteeringInput = 0;
    // }
    //
    // private void StopAccelerationInput()
    // {
    //     _playerMovement.AccelerationInput = 0;
    // }
}

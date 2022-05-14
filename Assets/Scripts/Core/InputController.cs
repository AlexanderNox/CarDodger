using System;
using UnityEngine;
using UnityEngine.InputSystem;



    [DefaultExecutionOrder(-1)]
    public class InputController : Singleton<InputController>
    {
        public delegate void StartMoveEvent(float force);
        public event StartMoveEvent OnStartSteering;
        public event StartMoveEvent OnStartAcceleration;
        public event Action OnPerformedSteering;
        public event Action OnPerformedAcceleration;
        
        
        private PlayerInputMap _playerInputMap;
        private InputAction _move;
    

        private void Awake()
        {
            _playerInputMap = new PlayerInputMap();
        }

        private void OnEnable()
        {
            _playerInputMap.Enable();
        }

        private void OnDisable()
        {
            _playerInputMap.Disable();
        }

        private void Start()
        {
            _playerInputMap.Player.Acceleration.started += ctx => StartAcceleration(ctx);
            _playerInputMap.Player.Acceleration.performed += _ => PerformedAcceleration();
            _playerInputMap.Player.Steering.started += ctx => StartSteering(ctx);
            _playerInputMap.Player.Steering.performed += _ => PerformedSteering();;

        }

        private void StartAcceleration(InputAction.CallbackContext context)
        {
            if (OnStartAcceleration != null)
            {
                OnStartAcceleration(_playerInputMap.Player.Acceleration.ReadValue<float>());
            }
        }
        
        private void StartSteering(InputAction.CallbackContext context)
        {
            if (OnStartSteering != null)
            {
                OnStartSteering(_playerInputMap.Player.Steering.ReadValue<float>());
            }
        }
        
        private void PerformedSteering()
        {
            if (OnPerformedSteering != null)
            {
                OnPerformedSteering();
            }
        }
        
        private void PerformedAcceleration()
        {
            if (OnPerformedAcceleration != null)
            {
                OnPerformedAcceleration();
            }
        }
    }


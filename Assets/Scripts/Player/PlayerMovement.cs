using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _driftFactor;
    [SerializeField] private float _accelerationFactor;
    [SerializeField] private float _turnFactor;
    [SerializeField] private float _minSpeedBeforeAllowTurningFactor;

    public float AccelerationInput {get; set;}
    public float SteeringInput {get; set;}
    
    private float _rotationAngle;
    private Rigidbody2D _playerRigidbody2D;

    void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        ApplyMoveForce();
        RemoveOrthogonalVelocity();
        ApplyRotation();
    }
    
    private void ApplyMoveForce()
    {
        // if (AccelerationInput == 0)
        // {
        //     _playerRigidbody2D.drag = Mathf.Lerp(_playerRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        // }
        // else
        // {
        //     _playerRigidbody2D.drag = 0;
        // }
        
        Vector2 moveForceVector = transform.up * AccelerationInput * _accelerationFactor;
        
        _playerRigidbody2D.AddForce(moveForceVector, ForceMode2D.Force);
    }

    private void ApplyRotation()
    {
        float minSpeedBeforeAllowTurning = (_playerRigidbody2D.velocity.magnitude / _minSpeedBeforeAllowTurningFactor);
        minSpeedBeforeAllowTurning = Mathf.Clamp01(minSpeedBeforeAllowTurning);
        
        _rotationAngle -= SteeringInput * _turnFactor * minSpeedBeforeAllowTurning;
        
        _playerRigidbody2D.MoveRotation(_rotationAngle);
    }

    void RemoveOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_playerRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_playerRigidbody2D.velocity, transform.right);
    
        _playerRigidbody2D.velocity = forwardVelocity + rightVelocity * _driftFactor;
    }
}

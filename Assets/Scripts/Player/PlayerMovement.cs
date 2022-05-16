using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _defaultDriftFactor;
    [SerializeField] private float _driftFactorInDriftMode;
    [SerializeField] private float _accelerationFactor;
    [SerializeField] private float _turnFactor;
    [SerializeField] private float _minSpeedBeforeAllowTurningFactor;
    [SerializeField] private float _brakingDistance;
    [SerializeField] private float _maxSpeed;

    private float _accelerationInput;
    private float _steeringInput;
    
    private float _driftFactor ;
    private float velocityVsUp;
    private float _rotationAngle;
    private Rigidbody2D _playerRigidbody2D;

    private bool driftMode { get; set; }

    void Awake()
    {
        _driftFactor = _defaultDriftFactor;
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        ApplyMoveForce();
        RemoveOrthogonalVelocity();
        ApplyRotation();
        _driftFactor = _defaultDriftFactor;
    }
    
    private void ApplyMoveForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, _playerRigidbody2D.velocity);

        if (velocityVsUp > _maxSpeed && _accelerationInput > 0)
        {
            return; 
        }

        if (velocityVsUp < -_maxSpeed * 0.5 && _accelerationInput < 0)
        {
            return;  
        }


        if (_playerRigidbody2D.velocity.sqrMagnitude > _maxSpeed * _maxSpeed && _accelerationInput > 0)
        {
            return;
        }
            
        if (_accelerationInput == 0)
        {
            BrakingPlayer();
        }
        else
        {
            _playerRigidbody2D.drag = 0;
        }
        
        Vector2 moveForceVector = transform.up * _accelerationInput * _accelerationFactor;
        
        _playerRigidbody2D.AddForce(moveForceVector, ForceMode2D.Force);
    }

    private void ApplyRotation()
    {
        float minSpeedBeforeAllowTurning = (_playerRigidbody2D.velocity.magnitude / _minSpeedBeforeAllowTurningFactor);
        minSpeedBeforeAllowTurning = Mathf.Clamp01(minSpeedBeforeAllowTurning);
        
        _rotationAngle -= _steeringInput * _turnFactor * minSpeedBeforeAllowTurning;
        
        _playerRigidbody2D.MoveRotation(_rotationAngle);
    }

    private void RemoveOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_playerRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_playerRigidbody2D.velocity, transform.right);
    
        _playerRigidbody2D.velocity = forwardVelocity + rightVelocity * _driftFactor;
    }

    public void ApplyDriftMode()
    {
        BrakingPlayer();
        _driftFactor = _driftFactorInDriftMode;
    }

    private void BrakingPlayer()
    {
        _playerRigidbody2D.drag = Mathf.Lerp(_playerRigidbody2D.drag, _brakingDistance, Time.fixedDeltaTime * 3);
    }
    
    public void SetMoveVector(Vector2 moveVector)
    {
        _accelerationInput = moveVector.y;
        _steeringInput = moveVector.x;
    }
    
    
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public bool DriftMode {private get;  set;}
        
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
    private float velocityVersusUp;
    private float _rotationAngle;
    private Rigidbody2D _playerRigidbody2D;

    void Awake()
    {
        _driftFactor = _defaultDriftFactor;
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        ApplyDriftMode();
        ApplyMoveForce();
        RemoveOrthogonalVelocity();
        ApplyRotation();
    }
    
    private void ApplyMoveForce()
    {
        velocityVersusUp = Vector2.Dot(transform.up, _playerRigidbody2D.velocity);

        if (velocityVersusUp > _maxSpeed && _accelerationInput > 0)
        {
            return; 
        }

        if (velocityVersusUp < -_maxSpeed * 0.5 && _accelerationInput < 0)
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
        if (DriftMode)
        {
            BrakingPlayer();
            _driftFactor = _driftFactorInDriftMode;
            

        }
        else
        {
            _driftFactor = _defaultDriftFactor;
           
        }

        DriftMode = false;
    }

    private void BrakingPlayer()
    {
        _playerRigidbody2D.drag = Mathf.Lerp(_playerRigidbody2D.drag, _brakingDistance, Time.fixedDeltaTime * 3);
    }
    
    public float GetVelocityMagnitude()
    {
        return _playerRigidbody2D.velocity.magnitude;
    }
    
    public void SetMoveVector(Vector2 moveVector)
    {
        _accelerationInput = moveVector.y;
        _steeringInput = moveVector.x;
    }
    
    private float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, _playerRigidbody2D.velocity);
    }
    
    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;
        if (_accelerationInput < 0 && velocityVersusUp > 0)
        {
            isBraking = true;
            return true;
        }
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;
        return false;
    }
    
  

    
    
}

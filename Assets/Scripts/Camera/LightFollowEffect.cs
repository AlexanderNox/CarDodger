using UnityEngine;

public class LightFollowEffect : MonoBehaviour
{
    [SerializeField] private Transform _followingTarget;
    [SerializeField] float _followFactor;
    private Vector3 _targetPreviousPosition;
    void Start()
    {
        _targetPreviousPosition = _followingTarget.position;
    }
    
    void Update()
    {
        Vector3 delta = _followingTarget.position - _targetPreviousPosition;
        _targetPreviousPosition = _followingTarget.position;
        
        transform.position += delta * _followFactor;
    }
    
}

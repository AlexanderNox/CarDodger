using System;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioSource _palyerHitAudioSource;
    [SerializeField] private AudioSource _engineAudioSource;
    [SerializeField] private AudioSource _tiresScreechingAudioSource;

    private PlayerMovement _playerMovement;
    private float _desiredEnginePitch = 0.5f; 
    private float _tireScreechPitch = 0.5f; 

    private void Awake()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }
    
    void Update()
    {
        UpadateEngineSFX();
        UpdateTiresScreechingSFX();
    }

    private void UpadateEngineSFX()
    {
        float velocityMagnitude = _playerMovement.GetVelocityMagnitude();
        
        float desiredEngineVolume = velocityMagnitude * 0.3f;

        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        _engineAudioSource.volume = Mathf.Lerp(_engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        _desiredEnginePitch = velocityMagnitude * 0.2f;
        _desiredEnginePitch = Mathf.Clamp(_desiredEnginePitch, 0.5f, 2f);
        _engineAudioSource.pitch = Mathf.Lerp(_engineAudioSource.pitch, _desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    private void UpdateTiresScreechingSFX()
    {
        if (_playerMovement.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                _tiresScreechingAudioSource.volume =
                    Mathf.Lerp(_tiresScreechingAudioSource.volume, 1.0f, Time.deltaTime * 10);
                _tireScreechPitch = Mathf.Lerp(_tireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                _tiresScreechingAudioSource.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                _tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else
        {
            _tiresScreechingAudioSource.volume = Mathf.Lerp(_tiresScreechingAudioSource.volume, 0, Time.deltaTime * 10);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Attack>())
        {
            _palyerHitAudioSource.Play();
            
        }
    }
}

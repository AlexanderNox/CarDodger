using System;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    [SerializeField] private AudioSource _engineAudioSource;
    [SerializeField] private float _hearingArea;
    
    private float _desiredEnginePitch = 0.5f;

    
    private void Update()
    {
        UpadateEngineSFX();
    }

    private void UpadateEngineSFX()
    {

        float distanceToListener = Vector2.Distance(transform.position, Camera.main.transform.position);
        
        distanceToListener = Mathf.Clamp(distanceToListener, _hearingArea, 0.0f) / _hearingArea;

        float desiredEngineVolume = Mathf.Lerp(0.0f,1.0f,distanceToListener);
        
        _engineAudioSource.volume = Mathf.Lerp(_engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        _desiredEnginePitch = distanceToListener ;
        _desiredEnginePitch = Mathf.Clamp(_desiredEnginePitch, 0.5f, 2f);
        _engineAudioSource.pitch = Mathf.Lerp(_engineAudioSource.pitch, _desiredEnginePitch, Time.deltaTime * 1.5f);
    }
}

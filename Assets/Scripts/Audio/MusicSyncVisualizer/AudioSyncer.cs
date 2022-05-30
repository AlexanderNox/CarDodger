using UnityEngine;


public class AudioSyncer : MonoBehaviour
{
    [SerializeField] private float _bias;
    [SerializeField] private float _timeStep;
    [SerializeField] protected float _timeToBeat;
    [SerializeField] protected float _restSmoothTime;
    

    private float _previousAudioValue;
    private float _audioValue;
    private float _timer;

    protected bool _isBeat;
    
    public virtual void OnBeat()
    {
        Debug.Log("beat");
        _timer = 0;
        _isBeat = true;
    }
    
    public virtual void OnUpdate()
    {
        _previousAudioValue = _audioValue;
        _audioValue = AudioSpectrum.SpectrumValue;
        
        if (_previousAudioValue > _bias &&
            _audioValue <= _bias)
        {
            if (_timer > _timeStep)
                OnBeat();
        }
        
        if (_previousAudioValue <= _bias &&
            _audioValue > _bias)
        {
            if (_timer > _timeStep)
                OnBeat();
        }

        _timer += Time.deltaTime;
    }

    private void Update()
    {
        OnUpdate();
    }

    
}

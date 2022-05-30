using UnityEngine;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour
{ 
    
    [SerializeField] private AudioMixerGroup _mixer;
    
    private SaveSystem _saveSystem;
    
    private void Awake()
    {
        _saveSystem = SaveSystem.Instance;
    }
    
    public void ChangeVolume(float volume)
    {
        _mixer.audioMixer.SetFloat("MasterVolume", Mathf.Clamp(volume, -60,20 ));
        _saveSystem.SaveAudioVolume(volume);
    }
}

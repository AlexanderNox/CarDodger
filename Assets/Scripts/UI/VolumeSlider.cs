using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private VolumeChanger _volumeChanger;
    [SerializeField] private Slider _slider;
    public void ChangeVolume()
    { 
        _volumeChanger.ChangeVolume(Mathf.Lerp(-60, 20, _slider.value));
    }
}


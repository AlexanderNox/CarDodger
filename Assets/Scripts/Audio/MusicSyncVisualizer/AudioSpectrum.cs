using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSpectrum : MonoBehaviour
{
    public static float SpectrumValue {get; private set;}
    
    private float[] _audioSpectrum;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSpectrum = new float[128];
    }
    
   private void Update()
       {
           // AudioListener.GetSpectrumData(_audioSpectrum, 1, FFTWindow.Hamming);
           _audioSource.GetSpectrumData(_audioSpectrum, 1, FFTWindow.Hamming);
           if (_audioSpectrum != null && _audioSpectrum.Length > 0)
           {
               SpectrumValue = _audioSpectrum[0] * 100;
           }
       }
   
      

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioClip _selectedSFX;
    [SerializeField] private AudioClip _pressedSFX;
    
    private AudioSource _audioSource;
    
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SelectedSound()
    {
        Debug.Log("Selected");
        _audioSource.PlayOneShot(_selectedSFX);
    }

    public void PressedSound()
    {
        Debug.Log("Pressed");
        _audioSource.PlayOneShot(_pressedSFX);
    }
}

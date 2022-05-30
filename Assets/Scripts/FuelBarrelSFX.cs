using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FuelBarrelSFX : MonoBehaviour
{
    [SerializeField] private AudioSource _pickupSFX;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>())
        {
            _pickupSFX.Play();
        }
    }
}

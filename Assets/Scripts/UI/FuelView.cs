using System;
using UnityEngine;
using UnityEngine.UI;

public class FuelView : MonoBehaviour
{
    [SerializeField] private Fuel _fuel;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.maxValue = _fuel.MaxFuel;
    }

    void Update()
    {
        _slider.value = _fuel.CurrentFuel;
    }
}

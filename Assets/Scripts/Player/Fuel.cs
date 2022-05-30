using System;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    public event Action OnFuelOver;
    public float CurrentFuel => _currentFuel;
    public float MaxFuel => _maxFuel;
        
    [SerializeField] private float _maxFuel;
    private float _currentFuel;
    private void Awake()
    {
        _currentFuel = _maxFuel;
    }
    
    public void TakeFuel(float fuel)
    {
        _currentFuel -= fuel;

        if (_currentFuel <= 0)
        {
            OnFuelOver?.Invoke();
            Destroy(gameObject);
        }
    }
    
    public void AddFuel(float fuel)
    {
        _currentFuel += fuel;

        if (_currentFuel > _maxFuel)
        {
            _currentFuel = _maxFuel;
        }
    }
}

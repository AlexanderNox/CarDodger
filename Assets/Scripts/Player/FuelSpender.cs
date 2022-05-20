using UnityEngine;

[RequireComponent(typeof(Fuel))]
public class FuelSpender : MonoBehaviour
{
    [SerializeField] private float _spendFactor;
    private Fuel _fuel;

    private void Awake()
    {
        _fuel = GetComponent<Fuel>();
    }

    private void FixedUpdate()
    {
        _fuel.TakeFuel(Time.deltaTime * _spendFactor);
    }
}
    


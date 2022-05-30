using UnityEngine;

[RequireComponent(typeof(Fuel))]
public class FuelSpender : MonoBehaviour
{
    [SerializeField] private float _spendSpeedEasy;
    [SerializeField] private float _spendSpeedNormal;
    [SerializeField] private float _spendSpeedHard;
    private float _spendSpeed;
    private Fuel _fuel;

    private void Awake()
    {
        switch (PlayerPrefs.GetString("DifficultyLevel"))
        {
            case "easy":
                _spendSpeed = _spendSpeedEasy; 
                break;
            case "normal":
                _spendSpeed = _spendSpeedNormal; 
                break;
            case "hard":
                _spendSpeed = _spendSpeedHard; 
                break;
        }
        _fuel = GetComponent<Fuel>();
    }

    private void FixedUpdate()
    {
        _fuel.TakeFuel(Time.deltaTime * _spendSpeed);
    }
    
    
}
    


using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private Fuel _fuel;
    [SerializeField] private GameObject[] _willOffObject;
    
    private SaveSystem _saveSystem;
    
    private void Awake()
    {
        _saveSystem = SaveSystem.Instance;
    }

    private void OnEnable()
    {
        _fuel.OnFuelOver += EndTheGame;
    }

    private void OnDisable()
    {
        _fuel.OnFuelOver -= EndTheGame;
    }

    public void EndTheGame()
    {
        foreach (var gameObject in _willOffObject)
        {
            gameObject.SetActive(false);
        }
        _gameOverPanel.SetActive(true);
        _saveSystem.SaveScore(_scoreCounter.TotalScore());
    }
}

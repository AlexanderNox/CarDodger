using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class LastAttempts : MonoBehaviour
{
    [SerializeField] private float _lineSpacing;
    [SerializeField] private GameObject _scoreResultPrefab;
    [SerializeField] private Transform _parent;

    private Vector3 _spawnPoint;
    
    
    private SaveSystem _saveSystem;

    private void Awake()
    {
        _spawnPoint = transform.position;
        _saveSystem = SaveSystem.Instance;
    }

    private void Update()
    {
        foreach (var scoreResult in _saveSystem.SaveProperty.ScoreResult)
        {
            Debug.Log("ScoreResult " + scoreResult);
        }
    }

    private void OnEnable()
    {
        foreach (var scoreResult in _saveSystem.SaveProperty.ScoreResult)
        {
            var scoreResultGameObject = Instantiate(_scoreResultPrefab, _spawnPoint, Quaternion.identity,_parent);
            // scoreResultGameObject.transform.localPosition = Vector3.zero;
            
            scoreResultGameObject.transform.position += (Vector3.down * _lineSpacing);
            _spawnPoint = scoreResultGameObject.transform.position;

            scoreResultGameObject.GetComponent<TextMeshProUGUI>().text = scoreResult.ToString();
        }
    }
}

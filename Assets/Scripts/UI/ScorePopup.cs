using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class ScorePopup : MonoBehaviour
{
  [SerializeField] private float _moveUpSpeed;
  [SerializeField] private float _lifetime;
  [SerializeField] private float _startDisappearancesTimeFactor;
  [SerializeField] private float _scaleFactor;
  [SerializeField] private AnimationCurve _animationCurve;

 

  private float _intialLifetime;
  private Color _textColor;
  private TextMeshPro _textMesh;
  private float _currentTime, _totalTime;
  

  private void Awake()
  {
    _intialLifetime = _lifetime;
    _textMesh = GetComponent<TextMeshPro>();
    _textColor = _textMesh.color;

    _totalTime = _animationCurve.keys[_animationCurve.keys.Length -1].time;
  }

  private void Update()
  {
  
    
    transform.position += new Vector3(0, _moveUpSpeed) * Time.deltaTime;

    _lifetime -= Time.deltaTime;
    transform.localScale += Vector3.one * _scaleFactor * Time.deltaTime * _animationCurve.Evaluate(_currentTime);
    
    if (_lifetime <= _intialLifetime / _startDisappearancesTimeFactor)
    {
      _textColor.a -= Time.deltaTime;
      _textMesh.color = _textColor;
      
      if (_lifetime <= 0)
      {
        Destroy(gameObject);
      }
    }
    
    _currentTime += Time.deltaTime;

    if (_currentTime >= _totalTime)
    {
      _currentTime = 0;
    }
    
    
  }

  public void Setup(String popupText)
  {
    _textMesh.text = popupText;
  }
  
  
}

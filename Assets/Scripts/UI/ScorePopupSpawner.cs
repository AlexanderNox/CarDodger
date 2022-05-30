using System;
using UnityEngine;

[RequireComponent(typeof(ScoreCounter))]
public class ScorePopupSpawner : MonoBehaviour
{
   [SerializeField] private Transform _popupPrefab;
   private ScoreCounter _scoreCounter;

   private void Awake()
   {
      _scoreCounter = GetComponent<ScoreCounter>();
   }

   private void OnEnable()
   {
      _scoreCounter.ScoreChange += ctx => SpawnAddPopup(ctx);
     
   }
   
   private void OnDisable()
   {
      _scoreCounter.ScoreChange -= SpawnAddPopup;
     
   }

   private void SpawnAddPopup(int score)
   {
      Transform scorePopupTransform = Instantiate(_popupPrefab, transform.position, Quaternion.identity);
      ScorePopup scorePopup = scorePopupTransform.GetComponent<ScorePopup>();

      if (score >= 0)
      {
         scorePopup.Setup("+ " + score);
      }
      else
      {
         scorePopup.Setup("- " + Mathf.Abs(score));
      }
      
   }
}

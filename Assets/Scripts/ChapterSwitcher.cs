using System;
using UnityEngine;

public class ChapterSwitcher : MonoBehaviour
{
  [SerializeField] private int _numberScoreToAccessChapter;
  [SerializeField] private ScoreCounter _scoreCounter;
  [SerializeField] private Animator _battelAnimator;

  private void OnEnable()
  {
    _scoreCounter.ScoreChange += ctx => SwitchChapter();
  }

  private void OnDisable()
  {
    _scoreCounter.ScoreChange -= ctx => SwitchChapter();
  }

  private void SwitchChapter()
  {
    if (_scoreCounter.Score >= 8000)
    {
      _scoreCounter.GetComponent<GameOver>().EndTheGame();
    }
    else if (_scoreCounter.Score >= _numberScoreToAccessChapter)
    {
      _battelAnimator.SetBool("ChangeChapter", true);
    }
 
  }
}

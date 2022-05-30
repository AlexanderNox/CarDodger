using UnityEngine;
using TMPro;

public class TotalScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ScoreCounter _scoreCounter;
    
    private void Update()
    {
        _text.text = _scoreCounter.TotalScore().ToString();
    }
}

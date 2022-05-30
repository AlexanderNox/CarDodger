using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public delegate void ScoreChanged(int score);

    public event ScoreChanged ScoreChange;

    public int Score => _score;
    private int _score;

    public void ChangeScore(int score)
    {
        _score += score;
        
        ScoreChange?.Invoke(score);
        
        if (_score < 0)
        {
            _score = 0;
        }
    }

    public int TotalScore()
    {
        switch (PlayerPrefs.GetString("DifficultyLevel"))
        {
            case "easy": 
                return _score / 2; 
            break;
            case "normal": 
                return _score; 
            break;
            case "hard":
                return _score * 2; 
            break;
        }
        return _score;
    }
}

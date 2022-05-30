using System;
using UnityEngine;

public class DifficuityLevelHandler : MonoBehaviour
{
    //0-easy 1-normal 2-hard
    public void SetDifficultyLevel(String difficultyLevel)
    {
        PlayerPrefs.SetString("DifficultyLevel", difficultyLevel);
    }
    
}

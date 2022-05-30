using UnityEngine;
using System.IO;

public class SaveSystem : Singleton<SaveSystem>
{

    public Save SaveProperty => _save;
    private Save _save = new Save();
    
    private string _path;
 
    private void Start()
    {
        
        _path = Path.Combine(Application.dataPath, "Save.json");
        
        _save = JsonUtility.FromJson<Save>(File.ReadAllText(_path));
        Debug.Log("Last trys score " + _save.ScoreResult);
        
        
    }

    public void SaveScore(int totalScore)
    {
        _save.ScoreResult.Add(totalScore);
    }
    
    public void SaveAudioVolume(float volume)
    {
        _save.SettingsSoundVolume = volume;
    }
    
    
    private void OnApplicationQuit()
    {
        File.WriteAllText(_path, JsonUtility.ToJson(_save));
    }
    
}

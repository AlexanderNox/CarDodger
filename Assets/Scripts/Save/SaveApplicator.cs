using UnityEngine;

public class SaveApplicator : MonoBehaviour
{
    [SerializeField] private VolumeChanger _volumeChanger;
    private SaveSystem _saveSystem;
    
    private void Awake()
    {
        _saveSystem = SaveSystem.Instance;
    }

    private void Update()
    {
        _volumeChanger.ChangeVolume(_saveSystem.SaveProperty.SettingsSoundVolume);
        gameObject.SetActive(false);
    }
}

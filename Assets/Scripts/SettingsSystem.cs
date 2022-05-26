using System;
using System.IO;
using UnityEngine;

public class SettingsSystem : MonoBehaviour
{
    private string path;
    
    private Settings _settings;
    public Settings Settings
    {
        get
        {
            if (_settings == null)
                GetOptions();
            
            return _settings;
        }
        set
        {
            _settings = value;
            UpdateOptions(value);
        }
    }

    private void UpdateOptions(Settings settings)
    {
        CheckDirrectory();
        
        File.WriteAllText($"{path}/Save.json", JsonUtility.ToJson(settings));
    }
    
    private void GetOptions()
    {
        CheckDirrectory();

        if (!File.Exists($"{path}/Save.json"))
        {
            _settings = new Settings();
            return;
        }
        
        string optionsJson = File.ReadAllText($"{path}/Save.json");
        _settings = JsonUtility.FromJson<Settings>(optionsJson);
    }

    private void CheckDirrectory()
    {
        if (string.IsNullOrEmpty(path))
            path = Path.Combine(Application.dataPath, "Json");
            
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}

[Serializable]
public class Settings
{
    public float volume = 1f;
    public bool musicEnabled = true;
}
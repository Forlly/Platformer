using System;
using System.IO;
using UnityEngine;

public class OptionsSystem : MonoBehaviour
{
    private string path;
    
    private Options options;
    public Options Options
    {
        get
        {
            if (options == null)
                GetOptions();
            
            return options;
        }
        set
        {
            options = value;
            UpdateOptions(value);
        }
    }

    private void UpdateOptions(Options _options)
    {
        CheckDirrectory();
        
        File.WriteAllText($"{path}/Save.json", JsonUtility.ToJson(_options));
    }
    
    private void GetOptions()
    {
        CheckDirrectory();

        if (!File.Exists($"{path}/Save.json"))
        {
            options = new Options();
            return;
        }
        
        string optionsJson = File.ReadAllText($"{path}/Save.json");
        options = JsonUtility.FromJson<Options>(optionsJson);
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
public class Options
{
    public float volume = 1f;
    public bool musicEnabled = true;
}
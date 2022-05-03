using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public Options _options = new Options();
    private string path;

    private void Awake()
    {
        path = Path.Combine(Application.dataPath, "Json");
        Debug.Log(path);

        if (Directory.Exists(path))
        {
            path = Path.Combine(path, "Save.json");
            _options = JsonUtility.FromJson<Options>(File.ReadAllText(path));
            Debug.Log(path);
            Debug.Log(_options.volume);
            Debug.Log(_options.musicEnabled);
        }
        else
        {
            Directory.CreateDirectory(path);
            path = Path.Combine(path, "Save.json");
            File.WriteAllText(path, JsonUtility.ToJson(_options));
        }
    }

    public void UpdateOptions(Options options)
    {
        File.WriteAllText(path, JsonUtility.ToJson(options));
    }
    
    public string GetOptions()
    {
        return File.ReadAllText(path);
    }
}

[Serializable]
public class Options
{
    public float volume = 1f;
    public bool musicEnabled = true;
}
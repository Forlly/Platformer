using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public void SaveFile<T>(T obj, string path, string fileName)
    {
        string fileJson = JsonUtility.ToJson(obj);
        File.WriteAllText($"{path}/{fileName}", fileJson);
    }
    public T LoadFile<T>(string path, string fileName)
    {
        string fileJson = File.ReadAllText($"{path}/{fileName}");
        return JsonUtility.FromJson<T>(fileJson);
    }
}

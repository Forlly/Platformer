using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveFile<T>(T obj, string path, string fileName)
    {
        string fileJson = JsonUtility.ToJson(obj);
        File.WriteAllText($"{path}/{fileName}", fileJson);
    }
    public static T LoadFile<T>(string path, string fileName)
    {
        string fileJson = File.ReadAllText($"{path}/{fileName}");
        return JsonUtility.FromJson<T>(fileJson);
    }
}

using System.IO;
using UnityEngine;

public static class DataStore<T>
{
    [ContextMenu("Write")]
    public static void SaveLocalData(T data, string fileName)
    {
        File.WriteAllText(DataPath(fileName), JsonUtility.ToJson(data));
    }

    [ContextMenu("Read")]
    public static T LoadLocalData(string fileName)
    {
        if (!File.Exists(DataPath(fileName))) return default;
        var fileContents = File.ReadAllText(Path.Combine(Application.persistentDataPath, fileName));
        T data = JsonUtility.FromJson<T>(fileContents);
        return data;
    }
    
    private static string DataPath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public static bool FileExists(string fileName)
    {
        return File.Exists(DataPath(fileName));
    }
}

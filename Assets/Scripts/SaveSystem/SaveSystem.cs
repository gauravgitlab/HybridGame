using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string BaseFolder = Path.Combine(Application.dataPath, "Progression");   
    
    public static void Save<T>(T data, string fileName) where T : SaveDataBase
    {
        if(data is ICustomSerializable serializable)
        {
            serializable.PrepareForSave();
        }
        
        string path = GetFilePath(fileName);
        EnsureFolder();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log($"Saved {typeof(T).Name} to {path}");
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
    
    public static T Load<T>(string fileName) where T : SaveDataBase, new()
    {
        string path = GetFilePath(fileName);

        T data;
        if (!File.Exists(path))
        {
            Debug.LogWarning($"Save file not found: {path}. Returning new instance.");
            data = new T();
        }
        else
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<T>(json);
        }
        
        if(data is ICustomSerializable serializable)
        {
            serializable.RestoreAfterLoad();
        }

        return data;
    }
    
    public static void Delete(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"Deleted save file: {path}");
        }
    }
    
    private static void EnsureFolder()
    {
        if (!Directory.Exists(BaseFolder))
        {
            Directory.CreateDirectory(BaseFolder);
        }
    }
    
    private static string GetFilePath(string fileName)
    {
        return Path.Combine(BaseFolder, $"{fileName}.json");
    }
}

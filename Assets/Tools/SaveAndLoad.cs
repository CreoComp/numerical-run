using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveAndLoad
{
    public static event Action loadedEvent;
    public static event Action saveEvent;
    public static event Action changeEvent;

    private static Dictionary<string, string> data;

    private static readonly string path = Application.persistentDataPath + "/data.json";

    static SaveAndLoad()
    {
        Load();
        Debug.Log(path);
    }

    public static bool Load()
    {
        if (File.Exists(path))
        //if (false)
        {
            string fileContents = File.ReadAllText(path);
            data = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileContents);

            Debug.Log("Game loaded!");

            if (loadedEvent != null)
                loadedEvent();

#if UNITY_EDITOR || UNITY_STANDALONE
            Set("coins", 1000000);
#endif

            return true;
        }
        else
        {
            Debug.Log("Save file not found!");
            data = new();

            if (loadedEvent != null)
                loadedEvent();

            return false;
        }
    }

    public static void Save()
    {
        string jsonString = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, jsonString);

        Debug.Log(jsonString);

        if (saveEvent != null)
            saveEvent();
        Debug.Log("Game saved!");
    }

    public static T Get<T>(string name, T startValue)
    {
        if (data.ContainsKey(name))
        {
            return JsonConvert.DeserializeObject<T>(data[name]);
        }
        else
        {
            data.Add(name, JsonConvert.SerializeObject(startValue));
            return startValue;
        }
    }

    public static void Set<T>(string name, T value)
    {
        if (data.ContainsKey(name))
        {
            if (changeEvent != null)
                changeEvent();
            data[name] = JsonConvert.SerializeObject(value);
        }
        else
        {
            data.Add(name, JsonConvert.SerializeObject(value));
        }
    }
}

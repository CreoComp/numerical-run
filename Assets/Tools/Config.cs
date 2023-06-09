using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Config
{
    private const string PATH = "Assets/config.cf";
    private static bool wasStarted = false;

    public static Dictionary<string, string> config = new();

    public static void Start()
    {
        if (wasStarted)
            return;

        string[] lines = File.ReadAllLines(PATH);
        foreach(string line in lines)
        {
            string key = "";
            string value = "";
            bool isKey = true;

            foreach(char i in line)
            {
                if (i == ':' && isKey)
                {
                    isKey = false;
                }
                else
                {
                    if (isKey)
                        key += i;
                    else
                        value += i;
                }
            }
            config.Add(key, value);
        }

        wasStarted = true;
    }

    public static string Get(string key)
    {
        return config[key];
    }
}

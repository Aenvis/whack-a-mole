using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveLoadHighscoreList
{
    public static void Save(string key, List<int> data)
    {
        string value = String.Join(',', data);
        
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static List<int> Load(string key)
    {
        List<int> result = new List<int>();

        string dataStr = PlayerPrefs.GetString(key);
        string[] data = dataStr.Split(',');

        foreach (var score in data)
        {
            if (Int32.TryParse(score, out int val))
            {
                result.Add(val);
            }
        }

        result.Sort();
        return result;
    }
}
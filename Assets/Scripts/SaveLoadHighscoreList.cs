using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveLoadHighscoreList
{
    public static void Save(string key, SortedSet<int> data)
    {
        string value = String.Join(',', data);
        
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static SortedSet<int> Load(string key)
    {
        SortedSet<int> result = new SortedSet<int>();

        string dataStr = PlayerPrefs.GetString(key);
        string[] data = dataStr.Split(',');

        foreach (var score in data)
        {
            if (Int32.TryParse(score, out int val))
            {
                result.Add(val);
            }
        }
        
        return result;
    }
}
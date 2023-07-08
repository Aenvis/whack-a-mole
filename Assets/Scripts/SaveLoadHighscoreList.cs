using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadHighscoreList
{
    public static void Save(string key, SortedSet<int> data)
    {
        var value = string.Join(',', data);

        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static SortedSet<int> Load(string key)
    {
        var result = new SortedSet<int>();

        var dataStr = PlayerPrefs.GetString(key);
        var data = dataStr.Split(',');

        foreach (var score in data)
            if (int.TryParse(score, out var val))
                result.Add(val);

        return result;
    }
}
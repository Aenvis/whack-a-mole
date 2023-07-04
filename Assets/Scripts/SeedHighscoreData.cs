using System.Collections.Generic;

public static class SeedHighscoreData
{
    public static void Initialize(List<int> highscoreList)
    {
        highscoreList.Add(1);
        highscoreList.Add(1);
        highscoreList.Add(1);
        highscoreList.Add(1);
        highscoreList.Sort();
    }
}

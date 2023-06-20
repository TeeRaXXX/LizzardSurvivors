using System.Collections.Generic;

public class TagsHandler
{
    private static List<string> _enemyTags = new List<string>()
    { 
        "Enemy",
        "SnailSmall",
        "SnailMedium"
    };

    private static string _playerTag = "Player";

    public static bool IsEnemy(string tag) => _enemyTags.Contains(tag);

    public static string GetPlayerTag() => _playerTag;
}
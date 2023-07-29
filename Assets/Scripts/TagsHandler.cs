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

    private static string _playerCamera = "MainCamera";

    private static string _destroyVolume = "DestroyVolume";

    public static string GetDropTag() => "Drop";
    public static string GetExperienceTakerTag() => "ExperienceTaker";

    public static bool IsEnemy(string tag) => _enemyTags.Contains(tag);

    public static string GetPlayerTag() => _playerTag;

    public static string GetPlayerCamera() => _playerCamera;

    public static string GetDestroyVolumeTag() => _destroyVolume;

    public static List<string> GetEnemyTags() => _enemyTags;
}
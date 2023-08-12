using System.Collections.Generic;

public class TagsHandler
{
    private static List<string> _enemyTags = new List<string>()
    {
        "Enemy",
        "SnailSmall",
        "SnailMedium"
    };

    private static List<string> _playerTags = new List<string>()
    {
        "Player",
        "Player0",
        "Player1",
        "Player2",
        "Player3"
    };

    private static string _playerCamera = "MainCamera";
    private static string _destroyVolume = "DestroyVolume";
    private static string _experienceKillVolume = "ExperienceKillVolume";
    private static string _skillsHolder = "SkillsHolder";
    private static string _ashes = "Ashes";
    private static string _playerHealthBar = "PlayerHealthBar";
    private static string _eventSystem = "EventSystem";

    public static string GetDropTag() => "Drop";
    public static string GetExperienceTakerTag() => "ExperienceTaker";
    public static bool IsEnemy(string tag) => _enemyTags.Contains(tag);
    public static string GetEventSystemTagTag() => _eventSystem;
    public static string GetPlayerHealthBarTag() => _playerHealthBar;
    public static string GetSkillsHolderTag() => _skillsHolder;
    public static string GetAshesTag() => _ashes;
    public static string GetPlayerCameraTag() => _playerCamera;
    public static string GetDestroyVolumeTag() => _destroyVolume;
    public static string GetDestroyExperienceTag() => _experienceKillVolume;
    public static List<string> GetEnemyTags() => _enemyTags;
    public static List<string> GetPlayerTags() => _playerTags;
}
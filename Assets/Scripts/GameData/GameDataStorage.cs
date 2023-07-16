using UnityEngine;

public class GameDataStorage : MonoBehaviour, IInitializeable
{
    public static GameDataStorage Instance { get; private set; }

    public GameData GameData { get; private set; }

    private GameDataFileHandler _fileHandler;

    public void Initialize()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
            Instance = this;

        if (_fileHandler == null)
            _fileHandler = new GameDataFileHandler(Application.persistentDataPath);

        LoadData();
    }

    public void SaveData()
    {
        if (GameData == null)
        {
            Debug.Log("Game data was not found. Initializing data to defaults!");
            ResetData();
        }

        _fileHandler.Save(GameData);
    }

    public GameData LoadData()
    {
        GameData = _fileHandler.Load();

        if (GameData == null)
        {
            Debug.Log("Game data was not found. Initializing data to defaults!");
            ResetData();
        }

        return GameData;
    }

    public void ResetData()
    {
        GameData = new GameData();
        GameData.AddOpenedCharacter(CharacterType.Dino);
        GameData.BuyCharacter(CharacterType.Dino);
        GameData.AddOpenedSkill(SkillType.AutoHeal, true);
        GameData.AddOpenedSkill(SkillType.MusicEvolved, true);
        GameData.AddOpenedSkill(SkillType.Music, true);
        SaveData();
    }
}
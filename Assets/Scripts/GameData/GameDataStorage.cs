using UnityEngine;

public class GameDataStorage
{
    private bool _isInitialized;

    public static GameDataStorage Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameDataStorage();
                _instance._isInitialized = false;
            }

            return _instance;
        }
        private set { }
    }

    public GameData GameData { get; private set; }

    private static GameDataStorage _instance;
    private GameDataFileHandler _fileHandler;

    public void Initialize()
    {
        if (_isInitialized)
            return;

        if (_fileHandler == null)
            _fileHandler = new GameDataFileHandler(Application.persistentDataPath);

        LoadData();
        _isInitialized = true;
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
        GameData.AddOpenedSkill(SkillType.IncreaseProjectileCount, true);
        GameData.AddOpenedSkill(SkillType.IncreaseAoeRadius, true);

        GameData.AddOpenedSkill(SkillType.TotemAoeDamage, true);
        GameData.AddOpenedSkill(SkillType.TotemAoeHeal, true);
        GameData.AddOpenedSkill(SkillType.MusicEvolved, true);
        GameData.AddOpenedSkill(SkillType.Music, true);

        SaveData();
    }
}
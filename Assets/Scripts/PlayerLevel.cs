using System;

public class PlayerLevel
{
    public static PlayerLevel Instance { get; private set; }

    private int _level;
    private float _experience;
    private float _experienceToLevelUp;

    public float Experience => _experience;
    public float Level => _level;

    private PlayerLevel() { }

    public static void Initialize(int startLevel)
    {
        if (Instance == null)
            Instance = new PlayerLevel();
        else return;

        if (startLevel < 0)
            throw new IndexOutOfRangeException($"startLevel equals {startLevel}");

        Instance._level = startLevel;
        Instance.UpdateExperienceToLevelUp();
        Instance._experience = 0f;
    }

    public void GetExperience(float experience)
    {
        if (experience <= 0)
            throw new IndexOutOfRangeException($"try to apply {experience} experience");

        _experience += experience;
        CheckExperience();
        EventManager.OnExperienceUpEvent(_experience, _experienceToLevelUp);
    }

    private void CheckExperience()
    {
        if (_experience >= _experienceToLevelUp)
        {
            _experience = 0;
            LevelUp();
        }
    }

    private void UpdateExperienceToLevelUp()
    {
        if (_level >= 1 && _level <= 5)
            _experienceToLevelUp = _level * 15;

        else if (_level >= 6 && _level <= 12)
            _experienceToLevelUp = _level * 30;

        else if (_level >= 13 && _level <= 20)
            _experienceToLevelUp = _level * 50;

        else if (_level >= 21 && _level <= 35)
            _experienceToLevelUp = _level * 90;

        else if (_level >= 36 && _level <= 55)
            _experienceToLevelUp = _level * 130;

        else if (_level >= 56 && _level <= 85)
            _experienceToLevelUp = _level * 160;

        else if (_level >= 86 && _level <= 115)
            _experienceToLevelUp = _level * 200;

        else if (_level >= 116 && _level <= 1000)
            _experienceToLevelUp = _level * 260;

        else if (_level >= 1001)
            _experienceToLevelUp = _level * 300;
    }

    private void LevelUp()
    {
        _level++;
        UpdateExperienceToLevelUp();
        EventManager.OnLevelUpEvent(_level);
    }
}
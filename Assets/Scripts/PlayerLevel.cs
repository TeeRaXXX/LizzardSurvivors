using System;

public class PlayerLevel
{
    public static PlayerLevel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerLevel();
            }

            return _instance;
        }
        private set { }
    }

    private static PlayerLevel _instance;
    private int _level;
    private int _experience;
    private int _experienceToLevelUp;

    public int Experience => _experience;
    public int Level => _level;

    private PlayerLevel() { }

    public void Initialize(int startLevel)
    {
        _level = startLevel;
        UpdateExperienceToLevelUp();
        _experience = 0;
    }

    public void GetExperience(int experience)
    {
        if (experience <= 0)
            throw new IndexOutOfRangeException($"try to apply {experience} experience");

        int expToAdd = experience;
        int levelsToAdd = 0;

        while (expToAdd > _experienceToLevelUp - _experience)
        {
            levelsToAdd++;
            expToAdd -= _experienceToLevelUp - _experience;
            _experience = 0;
            UpdateExperienceToLevelUp();
        }

        _experience += expToAdd;

        EventManager.OnExperienceUpEvent(_experience, _experienceToLevelUp);

        if (levelsToAdd > 0)
            LevelUp(levelsToAdd);
    }

    public void ResetLevel()
    {
        _instance = null;
    }

    private void LevelUp(int levelsToAdd)
    {
        _level += levelsToAdd;
        EventManager.OnLevelUpEvent(levelsToAdd);
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
}
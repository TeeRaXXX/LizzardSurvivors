using System;

public class PlayerLevel
{
    public static PlayerLevel Instance { get; private set; }

    private int _level;
    private float _experience;

    public float Experience => _experience;

    private PlayerLevel() { }

    public static void Initialize(int startLevel)
    {
        if (Instance == null)
            Instance = new PlayerLevel();
        else return;

        if (startLevel < 0)
            throw new IndexOutOfRangeException($"startLevel equals {startLevel}");

        Instance._level = startLevel;
        Instance._experience = 0f;
    }

    public void GetExperience(float experience)
    {
        if (experience <= 0)
            throw new IndexOutOfRangeException($"try to apply {experience} experience");

        _experience += experience;
        EventManager.OnExperienceUpEvent(_experience);
        CheckExperience();
    }

    public void CheckExperience()
    {

    }

    private void OnLevelUp()
    {
        _level++;
    }
}
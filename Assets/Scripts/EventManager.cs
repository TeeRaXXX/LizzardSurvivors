using UnityEngine.Events;

public static class EventManager
{
    public static readonly UnityEvent<float, float> OnPlayerHealthChanged = new UnityEvent<float, float>();
    public static readonly UnityEvent<SkillType> OnSkillAdded = new UnityEvent<SkillType>();
    public static readonly UnityEvent<EnemyType> OnEnemyDied = new UnityEvent<EnemyType>();
    public static readonly UnityEvent<int, int> OnSkillPointsAdded = new UnityEvent<int, int>();
    public static readonly UnityEvent<int> OnLevelUp = new UnityEvent<int>();
    public static readonly UnityEvent<int> OnNewGameSecond = new UnityEvent<int>();
    public static readonly UnityEvent<int> OnNewGameMinute = new UnityEvent<int>();
    public static readonly UnityEvent OnEscapePressed = new UnityEvent();

    public static void OnPlayerHealthChangedEvent(float newHealth, float maxHealth) =>
        OnPlayerHealthChanged.Invoke(newHealth, maxHealth);
    public static void OnSkillAddedEvent(SkillType skillType) => OnSkillAdded.Invoke(skillType);
    public static void OnEnemyDiedEvent(EnemyType enemyType) => OnEnemyDied.Invoke(enemyType);
    public static void OnSkillPointsAddedEvent(int skillPointsToAdd, int newSkillPointsValue)
        => OnSkillPointsAdded.Invoke(skillPointsToAdd, newSkillPointsValue);
    public static void OnLevelUpEvent(int newLevel) => OnLevelUp.Invoke(newLevel);
    public static void OnNewGameSecondEvent(int newSecond) => OnNewGameSecond.Invoke(newSecond);
    public static void OnNewGameMinuteEvent(int newMinute) => OnNewGameMinute.Invoke(newMinute);
    public static void OnEscapePressedEvent() => OnEscapePressed.Invoke();
}
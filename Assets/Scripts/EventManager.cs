using System.Collections.Generic;
using System.Numerics;
using UnityEngine.Events;

public static class EventManager
{
    public static readonly UnityEvent<float, float, int> OnPlayerHealthChanged = new UnityEvent<float, float, int>();
    public static readonly UnityEvent<SkillType, int, int> OnSkillAdded = new UnityEvent<SkillType, int, int>();
    public static readonly UnityEvent<SkillType, int, int> OnSkillDeleted = new UnityEvent<SkillType, int, int>();
    public static readonly UnityEvent<EnemyType> OnEnemyDied = new UnityEvent<EnemyType>();
    public static readonly UnityEvent<TimesOfDay> OnTimeOfDayChanged = new UnityEvent<TimesOfDay>();
    public static readonly UnityEvent<int> OnPlayerDied = new UnityEvent<int>();
    public static readonly UnityEvent<PlayerCharacter> OnPlayerInitialized = new UnityEvent<PlayerCharacter>();
    public static readonly UnityEvent<int, int> OnSkillPointsAdded = new UnityEvent<int, int>();
    public static readonly UnityEvent<int> OnLevelUp = new UnityEvent<int>();
    public static readonly UnityEvent<int> OnNewGameSecond = new UnityEvent<int>();
    public static readonly UnityEvent<int> OnNewGameMinute = new UnityEvent<int>();
    public static readonly UnityEvent<float, float> OnExperienceUp = new UnityEvent<float, float>();
    public static readonly UnityEvent OnPauseButtonPressed = new UnityEvent();
    public static readonly UnityEvent OnTimerStarted = new UnityEvent();
    public static readonly UnityEvent<int> OnChestPickUp = new UnityEvent<int>();
    public static readonly UnityEvent OnGameOver = new UnityEvent();
    public static readonly UnityEvent<ActionMaps> OnActionMapSwitch = new UnityEvent<ActionMaps>();
    public static readonly UnityEvent<bool> OnProjectilesUpdate = new UnityEvent<bool>();
    public static readonly UnityEvent<bool> OnAoeUpdate = new UnityEvent<bool>();
    public static readonly UnityEvent<List<SkillType>> OnNewSkillsOffer = new UnityEvent<List<SkillType>>();

    public static void OnPlayerHealthChangedEvent(float newHealth, float maxHealth, int playerIndex) =>
        OnPlayerHealthChanged.Invoke(newHealth, maxHealth, playerIndex);
    public static void OnSkillAddedEvent(SkillType skillType, int level, int playerIndex) => OnSkillAdded.Invoke(skillType, level, playerIndex);
    public static void OnSkillDeletedEvent(SkillType skillType, int level, int playerIndex) => OnSkillDeleted.Invoke(skillType, level, playerIndex);
    public static void OnEnemyDiedEvent(EnemyType enemyType) => OnEnemyDied.Invoke(enemyType);
    public static void OnTimeOfDayChangedEvent(TimesOfDay timeOfDay) => OnTimeOfDayChanged.Invoke(timeOfDay);
    public static void OnPlayerDiedEvent(int playerIndex) => OnPlayerDied.Invoke(playerIndex);
    public static void OnPlayerInitializedEvent(PlayerCharacter playerCharacter) => OnPlayerInitialized.Invoke(playerCharacter);
    public static void OnSkillPointsAddedEvent(int skillPointsToAdd, int newSkillPointsValue)
        => OnSkillPointsAdded.Invoke(skillPointsToAdd, newSkillPointsValue);
    public static void OnLevelUpEvent(int levelsToAdd) => OnLevelUp.Invoke(levelsToAdd);
    public static void OnExperienceUpEvent(float newExperience, float experienceToLevelUp) => OnExperienceUp.Invoke(newExperience, experienceToLevelUp);
    public static void OnNewGameSecondEvent(int newSecond) => OnNewGameSecond.Invoke(newSecond);
    public static void OnNewGameMinuteEvent(int newMinute) => OnNewGameMinute.Invoke(newMinute);
    public static void OnPauseButtonPressedEvent() => OnPauseButtonPressed.Invoke();
    public static void OnTimerStartedEvent() => OnTimerStarted.Invoke();
    public static void OnChestPickUpEvent(int playerIndex) => OnChestPickUp.Invoke(playerIndex);
    public static void OnGameOverEvent() => OnGameOver.Invoke();
    public static void OnActionMapSwitchEvent(ActionMaps actionMap) => OnActionMapSwitch.Invoke(actionMap);
    public static void OnProjectilesUpdateEvent(bool isNewLevel) => OnProjectilesUpdate.Invoke(isNewLevel);
    public static void OnAoeUpdateEvent(bool isNewLevel) => OnAoeUpdate.Invoke(isNewLevel);
    public static void OnNewSkillsOfferEvent(List<SkillType> newSkills) => OnNewSkillsOffer.Invoke(newSkills);
}
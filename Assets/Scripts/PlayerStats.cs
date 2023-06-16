using System;
using UnityEngine;

[Serializable] public class PlayerStats
{
    [SerializeField] private float _healthRecovery = 0f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _armor = 0f;
    [SerializeField] private float _moveSpeed = 2.5f;
    [SerializeField] private float _damageBonus = 0f;
    [SerializeField] private float _aoeRadiusBonus = 0f;
    [SerializeField] private float _cooldownReduceBonus = 0f;
    [SerializeField] private float _vampirismBonus = 0f;
    [SerializeField] private int _projectilesBonus  = 0;
    [SerializeField] private int _respawnTimes = 0;

    public float GetHealthRecovery() => _healthRecovery;
    public float GetMaxHealth() => _maxHealth;
    public float GetArmor() => _armor;
    public float GetMoveSpeed() => _moveSpeed;
    public float GetDamageBonus() => _damageBonus;
    public float GetAOERadiusBonus() => _aoeRadiusBonus;
    public float GetCooldownReduceBonus() => _cooldownReduceBonus;
    public float GetVampirismBonus() => _vampirismBonus;
    public int GetProjectilesBonus() => _projectilesBonus;
    public int GetRespawnTimes() => _respawnTimes;

    //public void SetHealthRecovery(float healthRecovery) => _healthRecovery = healthRecovery;
    //public void SetMaxHealth(float maxHealth) => _maxHealth = maxHealth;
    //public void SetArmor(float armor) => _armor = armor;
    //public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;
    //public void SetDamageBonus(float damageBonus) => _damageBonus = damageBonus;
    //public void SetAOERadiusBonus(float aoeRadiusBonus) => _aoeRadiusBonus = aoeRadiusBonus;
    //public void SetProjectilesBonus(int projectilesBonus) => _projectilesBonus = projectilesBonus;
    //public void SetCooldownReduceBonus(float cooldownReduceBonus) => _cooldownReduceBonus = cooldownReduceBonus;
    //public void SetVampirismBonus(float vampirismBonus) => _vampirismBonus = vampirismBonus;
    //public void SetRespawnTimes(int respawnTimes) => _respawnTimes = respawnTimes;

    public void InitStats(SOCharacter character)
    {
        _healthRecovery = character.CharacterBaseStats.GetHealthRecovery();
        _maxHealth = character.CharacterBaseStats.GetMaxHealth();
        _armor = character.CharacterBaseStats.GetArmor();
        _moveSpeed = character.CharacterBaseStats.GetMoveSpeed();
        _damageBonus = character.CharacterBaseStats.GetDamageBonus();
        _aoeRadiusBonus = character.CharacterBaseStats.GetAOERadiusBonus();
        _cooldownReduceBonus = character.CharacterBaseStats.GetCooldownReduceBonus();
        _vampirismBonus = character.CharacterBaseStats.GetVampirismBonus();
        _projectilesBonus = character.CharacterBaseStats.GetProjectilesBonus();
        _respawnTimes = character.CharacterBaseStats.GetRespawnTimes();
    }
}
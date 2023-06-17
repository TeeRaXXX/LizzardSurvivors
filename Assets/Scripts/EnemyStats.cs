using System;
using UnityEngine;

[Serializable] public class EnemyStats
{
    [SerializeField] private float _healthRecovery = 0f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _armor = 0f;
    [SerializeField] private float _moveSpeed = 2.5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackSpeed = 1f;

    public float GetHealthRecovery() => _healthRecovery;
    public float GetMaxHealth() => _maxHealth;
    public float GetArmor() => _armor;
    public float GetMoveSpeed() => _moveSpeed;
    public float GetDamage() => _damage;
    public float GetAttackSpeed() => _attackSpeed;

    public void InitStats(SOCharacter character)
    {
        _healthRecovery = character.CharacterBaseStats.GetHealthRecovery();
        _maxHealth = character.CharacterBaseStats.GetMaxHealth();
        _armor = character.CharacterBaseStats.GetArmor();
        _moveSpeed = character.CharacterBaseStats.GetMoveSpeed();
    }
}
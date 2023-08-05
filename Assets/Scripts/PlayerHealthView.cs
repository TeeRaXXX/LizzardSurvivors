using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private Transform _healthBar;

    public void Initialize()
    {
        EventManager.OnPlayerHealthChanged.AddListener(UpdateHealthBar);
    }

    public void UpdateHealthBar(float health, float maxHealth, int playerIndex)
    {
        _healthBar.transform.localScale = new Vector3(health / maxHealth, 1f, 1f);
    }
}
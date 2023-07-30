using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private Transform _healthBar;

    public void Initialize()
    {
        EventManager.OnPlayerHealthChanged.AddListener(UpdateHealthBar);
    }

    private void UpdateHealthBar(float health, float maxHealth)
    {
        _healthBar.transform.localScale = new Vector3(health / maxHealth, 1f, 1f);
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTakingDamageEffect : MonoBehaviour
{
    [SerializeField] private Color _damageEffectColor;
    [SerializeField] private float _effectDuration;

    private SpriteRenderer _spriteRenderer;
    private Shader _shaderGUItext;
    private Shader _defaultShader;
    private UnityEvent<float, float, GameObject> _onHealthChangedEvent;

    public void Initialize(SpriteRenderer spriteRenderer, UnityEvent<float, float, GameObject> onHealthChanged)
    {
        _shaderGUItext = Shader.Find("GUI/Text Shader");
        _defaultShader = Shader.Find("Sprites/Default");
        _spriteRenderer = spriteRenderer;
        _onHealthChangedEvent = onHealthChanged;
        onHealthChanged.AddListener(EnemyDamageEffect);
    }

    private void EnemyDamageEffect(float newHealth, float oldHealth, GameObject damageSource)
    {
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        _spriteRenderer.material.shader = _shaderGUItext;
        _spriteRenderer.color = _damageEffectColor;

        yield return new WaitForSeconds(_effectDuration);

        _spriteRenderer.material.shader = _defaultShader;
        _spriteRenderer.color = Color.white;
    }
}
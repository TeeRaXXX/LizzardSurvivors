using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialHeal : MonoBehaviour
{
    private List<string> _tagsToHeal;
    private float _heal = 5f;
    private float _healFrequency = 1f;
    private float _healRadius;

    private List<Collider2D> _objectsToHeal;
    private bool _isReadyToWork = false;

    private void Awake()
    {
        _isReadyToWork = false;
        _objectsToHeal = new List<Collider2D>();
    }

    public void Initialize(List<string> tagsToHeal, float healRadius, float heal, float healFrequency)
    {
        UpdateStats(tagsToHeal, healRadius, heal, healFrequency);
        _isReadyToWork = true;
    }

    public void UpdateStats(List<string> tagsToHeal, float healRadius, float heal, float healFrequency)
    {
        _tagsToHeal = new List<string>(tagsToHeal);
        _heal = heal;
        _healFrequency = healFrequency;
        _healRadius = healRadius;
    }

    private void FixedUpdate()
    {
        if (_objectsToHeal.Count > 0 && _isReadyToWork)
        {
            StartCoroutine(MakeHeal());
        }
    }

    private IEnumerator MakeHeal()
    {
        _isReadyToWork = false;

        for (int i = 0; i < _objectsToHeal.Count; i++)
        {
            if (_objectsToHeal[i] == null)
            {
                _objectsToHeal.Remove(_objectsToHeal[i]);
                continue;
            }

            var healthComponent = _objectsToHeal[i].GetComponent<HealthComponent>();
            healthComponent.ApplyHeal(_heal, gameObject);
        }

        yield return new WaitForSeconds(_healFrequency);
        _isReadyToWork = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isReadyToWork && other.GetComponent<HealthComponent>() != null && _tagsToHeal.Contains(other.gameObject.tag))
            _objectsToHeal.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_objectsToHeal.Contains(other))
            _objectsToHeal.Remove(other);
    }
}
using UnityEngine;

public class Experience : MonoBehaviour, IDroppable
{
    [SerializeField] private float _experienceCount;

    public void Drop(SOEnemy enemy)
    {
        _experienceCount = Random.Range(enemy.ExperienceMin, enemy.ExperienceMax);
    }

    public void OnTake()
    {
        PlayerLevel.Instance.GetExperience(_experienceCount);
        Destroy(gameObject);
    }

    public float GetExperienceCount => _experienceCount;
}
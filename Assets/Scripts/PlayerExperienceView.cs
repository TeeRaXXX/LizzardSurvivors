using UnityEngine;

public class PlayerExperienceView : MonoBehaviour
{
    [SerializeField] private Transform _experienceBar;
    [SerializeField] private float _maxBarValue;

    public void Initialize()
    {
        EventManager.OnExperienceUp.AddListener(UpdateExperienceBar);
        _experienceBar.transform.localScale = new Vector3(0f, 1f, 1f);
    }

    private void UpdateExperienceBar(float experience, float experienceToLevelUp)
    {
        _experienceBar.transform.localScale = new Vector3(experience / experienceToLevelUp * _maxBarValue, 1f, 1f);
    }
}
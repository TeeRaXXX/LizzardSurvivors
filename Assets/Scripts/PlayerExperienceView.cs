using UnityEngine;

public class PlayerExperienceView : MonoBehaviour, IInitializeable
{
    [SerializeField] private Transform _experienceBar;

    public void Initialize()
    {
        EventManager.OnExperienceUp.AddListener(UpdateExperienceBar);
        EventManager.OnLevelUp.AddListener(OnLevelUp);
        _experienceBar.transform.localScale = new Vector3(0f, 1f, 1f);
    }

    private void UpdateExperienceBar(float experience, float experienceToLevelUp)
    {
        _experienceBar.transform.localScale = new Vector3(experience / experienceToLevelUp, 1f, 1f);
    }

    private void OnLevelUp(int newLevel)
    {
        //_experienceBar.transform.localScale = new Vector3(0f, 1f, 1f);
    }
}
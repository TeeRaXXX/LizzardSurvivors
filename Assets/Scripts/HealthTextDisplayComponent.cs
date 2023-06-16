using UnityEngine;

public class HealthTextDisplayComponent : MonoBehaviour
{

    [SerializeField] private TextMesh TextMesh;
    private HealthComponent _healthComponent;

    private void Start()
    {
        _healthComponent = transform.parent.GetComponent<HealthComponent>();
    }

    void Update()
    {
        TextMesh.text = _healthComponent.GetHealth().ToString();
    }
}
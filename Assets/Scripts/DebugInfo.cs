using UnityEngine;
using TMPro;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private PlayerMovementComponent playerMovement;
    [SerializeField] private TMP_Text textMesh;

    void Update()
    {
        textMesh.text = $"HP - {healthComponent.GetHealth().ToString("#.#")}\n" +
            $"Look Vector - {playerMovement.GetLookDirection().x.ToString("0.0")} | {playerMovement.GetLookDirection().y.ToString("0.0")}\n" +
            $"Move Speed - {playerMovement.GetMoveSpeed().ToString("0.0")}";
    }
}

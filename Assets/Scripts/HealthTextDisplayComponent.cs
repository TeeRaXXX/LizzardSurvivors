using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTextDisplayComponent : MonoBehaviour
{

    [SerializeField] private TextMesh TextMesh;
    [SerializeField] private HealthComponent healthComponent;

    void Update()
    {
        TextMesh.text = healthComponent.GetHealth().ToString();
    }
}

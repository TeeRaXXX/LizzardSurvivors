using UnityEngine;

public class DropHeal : MonoBehaviour
{
    [SerializeField][Range(1f, 1000f)] private float _minHeal;
    [SerializeField][Range(1f, 1000f)] private float _maxHeal;

    private float _heal;

    private void Awake()
    {
        _heal = Random.Range(_minHeal, _maxHeal);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TagsHandler.GetPlayerTags().Contains(other.tag))
        {
            other.GetComponent<HealthComponent>().ApplyHeal(_heal, gameObject);
            Destroy(gameObject);
        }
    }
}
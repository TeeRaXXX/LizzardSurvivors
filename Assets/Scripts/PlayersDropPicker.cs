using UnityEngine;

public class PlayersDropPicker : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _pickupCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagsHandler.GetDropTag()))
        {
            IDroppable droppable;

            if (other.gameObject.TryGetComponent<IDroppable>(out droppable))
                droppable.OnTake();
        }
    }

    public void UpdatePickupRadius(float radius)
    {
        _pickupCollider.radius = radius;
    }
}
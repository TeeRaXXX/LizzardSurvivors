using UnityEngine;

public class PlayersDropPicker : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _pickupCollider;
    private int _playerIndex;

    public void Initialize(int playerIndex)
    {
        _playerIndex = playerIndex;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagsHandler.GetDropTag()))
        {
            IDroppable droppable;

            if (other.gameObject.TryGetComponent<IDroppable>(out droppable))
                droppable.OnTake(_playerIndex);
        }
    }

    public void UpdatePickupRadius(float radius)
    {
        _pickupCollider.radius = radius;
    }
}
using UnityEngine;

public class DropAshes : MonoBehaviour, IDroppable
{
    public void Drop(SOEnemy enemy) { }

    public void OnTake(int playerIndex) { }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == TagsHandler.GetDestroyVolumeTag())
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class Chest : MonoBehaviour, IDroppable
{
    public void Drop(SOEnemy enemy)
    {
        
    }

    public void OnTake(int playerIndex)
    {
        EventManager.OnChestPickUpEvent(playerIndex);
        Destroy(gameObject);
    }
}
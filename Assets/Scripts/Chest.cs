using UnityEngine;

public class Chest : MonoBehaviour, IDroppable
{
    public void Drop(SOEnemy enemy)
    {
        
    }

    public void OnTake()
    {
        EventManager.OnChestPickUpEvent();

        Destroy(gameObject);
    }
}
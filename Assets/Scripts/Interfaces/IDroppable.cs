using System;
using UnityEngine;

public interface IDroppable
{
    void Drop(SOEnemy enemy);

    void OnTake(int playerIndex);
}
using UnityEngine;

public class BuffsHandler : MonoBehaviour
{
    private float _moveSpeedBuffPercent;
    private Buffs _buffs;

    public void Initialize(Buffs baffs)
    {
        _buffs = baffs;
        _moveSpeedBuffPercent = 0;
    }

    public void IncreaseMoveSpeed(float speedPercentToReduce)
    {
        _moveSpeedBuffPercent += speedPercentToReduce;
    }

    public float GetMoveSpeedBuff() => _moveSpeedBuffPercent;
}
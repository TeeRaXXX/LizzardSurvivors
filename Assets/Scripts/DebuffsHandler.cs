using System.Collections.Generic;
using UnityEngine;

public class DebuffsHandler : MonoBehaviour
{
    private Dictionary<IDebuff, float> _moveSpeedDebuffs;
    private float _maxMoveSpeedDebuff;
    private Debuffs _debaffs;

    public void Initialize(Debuffs debaffs)
    {
        _debaffs = debaffs;
        _maxMoveSpeedDebuff = 0.9f;
        _moveSpeedDebuffs = new Dictionary<IDebuff, float>();
    }

    public void AddMoveSpeedDebuff(IDebuff debuff, float speedPercentToReduce)
    {
        if (!_moveSpeedDebuffs.ContainsKey(debuff) || _moveSpeedDebuffs.ContainsKey(debuff) && debuff.IsStackable())
        {
            _debaffs.AddDebuff(debuff);
            _moveSpeedDebuffs.Add(debuff, speedPercentToReduce);
        }
    }

    public void RemoveMoveSpeedDebuff(IDebuff debuff, float speedPercentToReduce)
    {
        if (_moveSpeedDebuffs.ContainsKey(debuff))
        {
            _debaffs.RemoveDebuff(debuff);
            _moveSpeedDebuffs.Remove(debuff);
        }
    }

    public float GetMoveSpeedDebuff()
    {
        float speedDebuff = 0f;
        
        foreach (var debuff in _moveSpeedDebuffs)
        {
            speedDebuff += debuff.Value;

            if (speedDebuff > _maxMoveSpeedDebuff)
                return _maxMoveSpeedDebuff;
        }

        return speedDebuff;
    }
}
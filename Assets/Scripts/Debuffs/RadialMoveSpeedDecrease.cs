using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMoveSpeedDecrease : MonoBehaviour, IDebuff
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SOBuffDebuffInfo _soBuffDebuffInfo;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private string _debuffName;

    private List<string> _tagsToDebuff;
    private List<DebuffsHandler> _enemiesDebuffs;
    private Sprite _logo;
    private string _name;
    private string _description;
    private bool _isStackable;

    private float _moveSpeedReducePercent;
    private float _lifeTime;

    ~RadialMoveSpeedDecrease()
    {
        End();
    }

    public void Initialize(float moveSpeedReducePercent, List<string> tagsToDebuff, float moveSpeedReduceRadius, float lifeTime = -1)
    {
        _enemiesDebuffs = new List<DebuffsHandler>();
        _moveSpeedReducePercent = moveSpeedReducePercent;
        _tagsToDebuff = tagsToDebuff;
        transform.localScale = new Vector3(moveSpeedReduceRadius, moveSpeedReduceRadius, 1f);
        InitDebuff(_soBuffDebuffInfo.BuffsDebuffs.Find(obj => obj.Name == _debuffName));
        if (lifeTime > 0) 
        {
            _lifeTime = lifeTime;
            StartCoroutine(OnLifeTime());
        }
    }

    public void UpdateStats(float moveSpeedReducePercent, List<string> tagsToDebuff, float moveSpeedReduceRadius)
    {
        _enemiesDebuffs = new List<DebuffsHandler>();
        _moveSpeedReducePercent = moveSpeedReducePercent;
        _tagsToDebuff = tagsToDebuff;
        transform.localScale = new Vector3(moveSpeedReduceRadius, moveSpeedReduceRadius, 1f);
    }

    public void InitDebuff(BuffDebuffInfoSO info)
    {
        _logo = info.Logo;
        _name = info.Name;
        _description = info.Description;
        _isStackable = false;
    }

    public void Start()
    {

    }

    public void End()
    {
        foreach (var debuffComponent in _enemiesDebuffs)
        {
            _enemiesDebuffs.Remove(debuffComponent);
            debuffComponent.RemoveMoveSpeedDebuff(this, _moveSpeedReducePercent);
        }
    }

    private IEnumerator OnLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime - 1f);
        _animator.SetBool("OnDeath", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_tagsToDebuff.Contains(other.tag))
        {
            DebuffsHandler debuffComponent = null;

            if (other.TryGetComponent<DebuffsHandler>(out debuffComponent))
            {
                _enemiesDebuffs.Add(debuffComponent);
                debuffComponent.AddMoveSpeedDebuff(this, _moveSpeedReducePercent);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_tagsToDebuff.Contains(other.tag))
        {
            DebuffsHandler debuffComponent = null;

            if (other.TryGetComponent<DebuffsHandler>(out debuffComponent))
            {
                _enemiesDebuffs.Remove(debuffComponent);
                debuffComponent.RemoveMoveSpeedDebuff(this, _moveSpeedReducePercent);
            }
        }
    }

    public string GetDescription() => _description;

    public Sprite GetLogo() => _logo;

    public string GetName() => _name;

    public bool IsStackable() => _isStackable;
}
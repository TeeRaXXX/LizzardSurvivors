using NastyDoll.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NecromantBehavior : MonoBehaviour, IEnemyBehavior
{
    [SerializeField] private float respawnCooldown;
    [SerializeField] private float respawnRadius;
    [SerializeField] AnimationEventHandler animationEventHandler;

    private UnityEvent _skillUseEvent;
    private EnemyCharacter _enemyCharacter;
    private bool _isReadyToWork;
    private string _ashesTag;

    private void Awake() => _isReadyToWork = false;

    public void Initialize(EnemyCharacter enemyCharacter)
    {
        _enemyCharacter = enemyCharacter;
        _skillUseEvent = animationEventHandler.GetVoidEvent();
        _skillUseEvent.AddListener(OnRespawn);
        _ashesTag = TagsHandler.GetAshesTag();
        _isReadyToWork = true;
        _enemyCharacter.Animator.SetBool("IsCastingSkill", true);
    }

    private void OnRespawn()
    {
        if (_isReadyToWork)
        {
            SoundManager.Instance.PlaySFX("NecromantSkill");
            List <GameObject> ashes = UtilsClass.GetObjectsInRadius(respawnRadius, transform.position, _ashesTag);
            if (ashes != null)
                foreach (var ash in ashes)
                {
                    EnemiesSpawnHandler.Instance.SpawnEnemy(EnemyType.Skeleton, ash.transform.position);
                    Destroy(ash);
                }

            StartCoroutine(Cooldown());
        }
        else
        {
            _enemyCharacter.Animator.SetBool("IsCastingSkill", false);

        }
    }

    private IEnumerator Cooldown() 
    {
        _isReadyToWork = false;
        _enemyCharacter.Animator.SetBool("IsCastingSkill", false);
        yield return new WaitForSeconds(respawnCooldown);
        _isReadyToWork = true;
        _enemyCharacter.Animator.SetBool("IsCastingSkill", true);
    }
}
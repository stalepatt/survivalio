using System;
using UnityEngine;

public class EnemyCharacter : CharacterBase, ISpawnable
{
    private Transform _target;

    public event Action OnDie;

    public override bool Init(Define.CharacterType characterType = Define.CharacterType.Enemy01)
    {
        SetInitialStat(characterType);
        gameObject.GetOrAddComponent<EnemyAnimationController>().Init();

        if (base.Init(characterType) == false)
        {
            return false;
        }

        _renderer.sortingOrder = 4;
        _target = Managers.GameManager.PlayerCharacter.transform;

        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
            player.TakeDamage(this.Stat.Attack);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    protected override bool IsReverseDirection()
    {
        if (_target == null)
        {
            return false;
        }

        return _target.position.x < this.transform.position.x;
    }

    protected override void SetTargetPosition()
    {
        _target = Managers.GameManager.PlayerCharacter.transform;
        if (_target == null)
        {
            Debug.Log("target null error");
        }
        _targetPosition = (_target.position - this.transform.position).normalized * (Stat.Speed * Time.fixedDeltaTime);
    }

    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        transform.position = _target.position + spawnPosition;
    }
    protected override void Die()
    {
        OnDie.Invoke();
    }

    public void Spawn()
    {
        Managers.PoolManager.EnemyPool.Get();
    }

    public void Return()
    {
        Managers.PoolManager.EnemyPool.Release(this);
    }

    public Define.SpawnableType GetSpawnableObjectType()
    {
        return Define.SpawnableType.Enemy;
    }

    public int GetAnimIndex()
    {
        int indexOffset = (int)Define.CharacterType.Enemy01 - 1;
        int enemyIndex = Mathf.Max(1, Stat.CharacterID - indexOffset);

        return enemyIndex;
    }

    private void OnDisable()
    {
        Return();
    }
}

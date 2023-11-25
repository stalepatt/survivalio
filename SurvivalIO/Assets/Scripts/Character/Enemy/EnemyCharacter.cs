using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : CharacterBase, ISpawnable
{
    private Transform _target;
    public event Action OnDie;
    public override bool Init(Define.CharacterType characterType = Define.CharacterType.Enemy01)
    {
        if (base.Init(Define.CharacterType.Enemy01) == false)
        {
            return false;
        }

        _renderer.sortingOrder = 4;

        _target = Managers.GameManager.Player.transform;        

        return true;
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
        _target = Managers.GameManager.Player.transform;
        if (_target == null)
        {
            Debug.Log("target null error");
        }
        _targetPosition = (_target.position - this.transform.position) * (Stat.Speed * Time.fixedDeltaTime);
    }

    protected override void Die()
    {
        OnDie.Invoke();
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public void Return()
    {
        throw new System.NotImplementedException();
    }

    public Define.SpawnableType GetSpawnableObjectType()
    {
        return Define.SpawnableType.Enemy;
    }
        
    private const int INDEX_OFFSET = 1; // CharacterID => 0 : Default, 1 : Player
    public int GetIndex()
    {
        int enemyIndex = Mathf.Max(INDEX_OFFSET, Stat.CharacterID - INDEX_OFFSET);
        return enemyIndex;
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

public abstract class CharacterBase : MonoBehaviour, IDamagable
{
    private bool _init = false;

    private Rigidbody2D _rigidbody;
    protected SpriteRenderer _renderer;
    protected Vector2 _targetPosition;
    protected Animator _animator;

    public CharacterData Stat { get; private set; }


    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        SetTargetPosition();
        if (Stat.Speed != 0 && _targetPosition != _rigidbody.position)
        {
            Move();
        }
    }

    private void LateUpdate()
    {
        _renderer.flipX = IsReverseDirection();
    }

    public virtual bool Init(Define.CharacterType characterType = Define.CharacterType.Default)
    {
        if (_init)
        {
            return false;
        }

        SetInitialStat(characterType);

        _rigidbody = gameObject.GetOrAddComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
        _rigidbody.gravityScale = 0;

        _renderer = gameObject.GetOrAddComponent<SpriteRenderer>();

        _animator = gameObject.GetOrAddComponent<Animator>();

        CharacterDebugModule debug = Utils.GetOrAddComponent<CharacterDebugModule>(gameObject);

        return _init = true;
    }

    protected CharacterData SetInitialStat(Define.CharacterType character)
    {
        Stat = Managers.DataManager.CharacterDatas[character].Clone();
        transform.localScale *= Stat.Scale;
        GetSkill(Stat.DefaultSkill);
        return Stat;
    }

    public void GetSkill(string name)
    {
        Stat.SetSkill(name);
    }
    protected abstract void SetTargetPosition();

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _targetPosition);
    }

    protected abstract bool IsReverseDirection();

    public void TakeDamage(int damageAmount)
    {
        Stat.HP -= damageAmount;

        if (Stat.HP <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IDamagable
{
    private bool _init = false;

    private Rigidbody2D _rigidbody;
    protected SpriteRenderer _renderer;
    protected Vector2 _targetPosition;
    protected Animator _animator;

    public CharacterStatData Stat { get; private set; }
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

    private void Awake()
    {
        Init();
    }

    private void FixedUpdate()
    {
        SetTargetPosition();
        if (Stat.Speed != 0 && _targetPosition != _rigidbody.position)
        {
            Move();
        }
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _targetPosition);
    }

    protected abstract void SetTargetPosition();

    private void LateUpdate()
    {
        _renderer.flipX = IsReverseDirection();
    }
    protected abstract bool IsReverseDirection();
    protected CharacterStatData SetInitialStat(Define.CharacterType character)
    {
        Stat = Managers.DataManager.CharacterStats[(int)character].Clone();
        GetSkill(Stat.DefaultSkill);
        return Stat;
    }

    public void GetSkill(string name)
    {
        Stat.SetSkill(name);
    }

    public void TakeDamage(int damageAmount)
    {
        Stat.Hp -= damageAmount;

        if (Stat.Hp <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}

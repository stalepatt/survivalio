using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public abstract class CharacterBase : MonoBehaviour, IDamagable, IMovable
{
    private bool _init = false;

    private Rigidbody2D _rigidbody;
    protected SpriteRenderer _renderer;
    protected Vector2 _targetPosition;
    protected Animator _animator;

    public CharacterData Stat { get; private set; }
    public Dictionary<string, Skill> Skills { get; private set; }
    public Transform SkillSet { get; private set; }

    private const int MAX_SKILL_COUNT = 12;

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

        _rigidbody = gameObject.GetOrAddComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
        _rigidbody.gravityScale = 0;

        _renderer = gameObject.GetOrAddComponent<SpriteRenderer>();

        _animator = gameObject.GetOrAddComponent<Animator>();

        SetInitialStat(characterType);

        return _init = true;
    }

    protected CharacterData SetInitialStat(Define.CharacterType character)
    {
        Stat = Managers.DataManager.CharacterDatas[character].Clone();
        transform.localScale = Vector3.one * Stat.Scale;

        return Stat;
    }

    public Skill GetSkill(string name)
    {
        if (name == "null")
        {
            return null;
        }

        if (Skills == null)
        {
            Skills = new Dictionary<string, Skill>(MAX_SKILL_COUNT);

            SkillSet = new GameObject("@SkillSet").transform;
            SkillSet.SetParent(this.gameObject.transform);
        }

        if (false == Skills.ContainsKey(name))
        {
            Skill newSkill =
                Managers.ResourceManager.Instantiate($"Ingame/{name}", SkillSet)
                .GetOrAddComponent<Skill>();

            newSkill.Init(name);
            Skills.Add(name, newSkill);

            return newSkill;
        }
        else
        {
            return Skills[name];
        }
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

    void IMovable.Move()
    {
        Move();
    }

    public void MoveInSpeed(int speedDiff)
    {
        int accelatedSpeed = Stat.Speed * speedDiff;
        _rigidbody.MovePosition((_rigidbody.position + _targetPosition) * accelatedSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    private bool _init = false;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    protected Vector2 _targetPosition;

    public CharacterStatData Stat { get; set; }
    public Vector3 Position { get { return this.transform.position; } }
    public virtual bool Init()
    {
        if (_init)
        {
            return false;
        }

        _rigidbody = gameObject.GetOrAddComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
        _rigidbody.gravityScale = 0;

        _renderer = gameObject.GetOrAddComponent<SpriteRenderer>();
        //_renderer.sprite = Managers.ResourceManager.Load<Sprite>("Character sprite Path");

        GetStatData();

        return _init = true;
    }

    private void FixedUpdate()
    {
        if (_targetPosition != _rigidbody.position)
        {
            Move();
        }
    }

    private void Move()
    {
        SetTargetPosition();
        _rigidbody.MovePosition(_rigidbody.position + _targetPosition);
    }

    protected abstract void SetTargetPosition();

    private void LateUpdate()
    {
        _renderer.flipX = IsReverseDirection();
    }
    protected abstract bool IsReverseDirection();

    protected CharacterStatData GetStatData() // юс╫ц
    {
        if (Stat == null)
        {
            Stat = new CharacterStatData();
            Stat.Speed = 10;
        }

        return Stat;
    }
}

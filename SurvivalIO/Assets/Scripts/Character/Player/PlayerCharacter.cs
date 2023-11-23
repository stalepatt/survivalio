using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase
{
    private PlayerInput _input;
    private Vector2 _inputVector;
    private bool _isReversed;

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false) // already initialized
        {
            return false;
        }

        _input = Utils.GetOrAddComponent<PlayerInput>(gameObject);

        return true;
    }

    protected override void SetTargetPosition()
    {
        _targetPosition = _inputVector * Stat.Speed * Time.fixedDeltaTime;
    }

    protected override bool IsReverseDirection()
    {
        if (_inputVector.x != 0)
        {
            _isReversed = _inputVector.x < 0;
            return _isReversed;
        }

        return _isReversed;
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
        SetTargetPosition();
    }
}

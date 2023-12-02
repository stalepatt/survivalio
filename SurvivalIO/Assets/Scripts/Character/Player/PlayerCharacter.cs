using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase
{
    private PlayerInput _input;
    private Vector2 _inputVector;
    private bool _isReversed;
    private int _currentExp = DEFAULT_EXP;
    private int _currentLevelUpExp = DEFAULT_LEVEL_UP_EXP;
    private int _level = DEFAULT_LEVEL;

    private const int DEFAULT_EXP = 0;
    private const int DEFAULT_LEVEL = 1;
    private const int DEFAULT_LEVEL_UP_EXP = 10;

    public int CurrentExp { get => _currentExp; private set => _currentExp = value; }
    public int CurrentLevelUpExp { get => _currentLevelUpExp; private set => _currentLevelUpExp = value; }
    public int Level { get => _level; private set => _level = value; }

    public event Action OnDie;

    private void Start()
    {
        Init(Define.CharacterType.Player);
    }

    public override bool Init(Define.CharacterType character = Define.CharacterType.Player)
    {
        if (base.Init(Define.CharacterType.Player) == false) // already initialized
        {
            return false;
        }

        Managers.GameManager.PlayerCharacter = this;

        OnDie -= Managers.GameManager.EndGame;
        OnDie += Managers.GameManager.EndGame;

        _renderer.sortingOrder = 10;
        _input = Utils.GetOrAddComponent<PlayerInput>(gameObject);

        GetSkill(Stat.DefaultSkill);

        return true;
    }

    protected override void SetTargetPosition()
    {
        _targetPosition = _inputVector * (Stat.Speed * Time.fixedDeltaTime);
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

    protected override void Die()
    {
        OnDie.Invoke();
    }

    private const int LEVEL_UP_EXP_INCREASEMENT = 2;
    public void GetExp(int expAmount)
    {
        _currentExp += expAmount;

        if (_currentExp >= _currentLevelUpExp)
        {
            int overbalanceExp = _currentExp - _currentLevelUpExp;

            LevelUp();

            int newLevelUpExp = _currentLevelUpExp * LEVEL_UP_EXP_INCREASEMENT;
            _currentLevelUpExp = newLevelUpExp;

            GetExp(overbalanceExp);
        }

        Managers.UIManager.FindPopup<IngameBattlePopup>()
            .SetExpBar(_currentExp, _currentLevelUpExp);
    }

    private void LevelUp()
    {
        ++_level;
        _currentExp = DEFAULT_EXP;
        // selectSkillPopup
        Managers.UIManager.FindPopup<IngameBattlePopup>()
            .SetLevelText(Level);
    }

}

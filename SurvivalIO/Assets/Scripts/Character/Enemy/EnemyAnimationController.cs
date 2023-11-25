﻿using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private readonly static int Idle = Animator.StringToHash("Idle");
    private readonly static int Die = Animator.StringToHash("Die");

    private EnemyCharacter _enemyCharacter;

    private Animator _animator;
    private AnimatorOverrideController _animatorOverrideController;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _enemyCharacter = Utils.GetOrAddComponent<EnemyCharacter>(gameObject);               
        _enemyCharacter.OnDie -= () => SetBool(true);
        _enemyCharacter.OnDie += () => SetBool(true);

        _animator = Utils.GetOrAddComponent<Animator>(gameObject);
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;        
                
        _animatorOverrideController["Idle"] = Managers.ResourceManager.Load<AnimationClip>($"Anim/Enemy/Idle/Idle_{_enemyCharacter.GetIndex()}"); // name 자리에 ID 필요
        _animatorOverrideController["Die"] = Managers.ResourceManager.Load<AnimationClip>($"Anim/Enemy/Die/Die_{_enemyCharacter.GetIndex()}");        
    }

    public void SetBool(bool value)
    {
        _animator.SetBool(Die, value);
    }
}
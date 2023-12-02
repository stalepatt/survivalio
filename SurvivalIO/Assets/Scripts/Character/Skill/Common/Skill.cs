using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected int _currentSkiilLevel;
    protected List<SkillData> _dataList;
    private const int MAX_SKILL_LEVEL = 5;
    public void Init(string name)
    {
        if (Managers.PoolManager.BulletPool.Root == null)
        {
            Managers.PoolManager.BulletPool.Init(new GameObject("BulletPool"), Managers.PoolManager.ObjectContainer);
        }

        _currentSkiilLevel = 1;
        _dataList = Managers.DataManager.SkillDatas[name];
        Attack();
    }

    public abstract void Attack();
    protected abstract void Fire();

    public void LevelUp()
    {
        ++_currentSkiilLevel;
    }

    public SkillData GetCurrentSkillLevelData()
    {
        return _dataList[_currentSkiilLevel];
    }

    public SkillData GetNextLevelData()
    {
        if (_currentSkiilLevel == MAX_SKILL_LEVEL)
        {
            return null;
        }

        return _dataList[_currentSkiilLevel + 1];
    }
}
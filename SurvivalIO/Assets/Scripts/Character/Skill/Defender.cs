using System;
using Cysharp.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Defender : Skill
{
    private SkillData _data;
    private float[] _angles;

    public override void Attack()
    {
        AttackTask().Forget();
    }

    public async UniTaskVoid AttackTask()
    {
        while (Managers.SceneManager.CurrentSceneType == Define.Scene.InGame)
        {
            Fire();
            await UniTask.Delay(TimeSpan.FromSeconds(_data.AttackInterval));
        }
    }

    private const float DISTANCE_FROM_PLAYER = 1.5f;
    protected override void Fire()
    {
        _data = GetCurrentSkillLevelData();

        Utils.GetAngleFromCircleDivide(_data.ProjectileCount, out _angles);

        for (int bulletCount = 0; bulletCount < _data.ProjectileCount; ++bulletCount)
        {
            Bullet projectile = Managers.PoolManager.BulletPool.Get();
            projectile.Init(this.transform, _data);
            SetProjectilePosition(projectile.transform, bulletCount);
        }

        DefenderPatternTask().Forget();
    }

    private const float DEFAULT_SKILL_SPEED = 200f;
    private readonly Vector3 ROTATE_DIRECTION = Vector3.forward;
    public async UniTaskVoid DefenderPatternTask()
    {
        GameObject bullet = transform.GetChild(0).gameObject;
        while (bullet.activeSelf)
        {
            float speed = DEFAULT_SKILL_SPEED * _data.Speed;
            transform.Rotate(ROTATE_DIRECTION * speed * Time.deltaTime);

            await UniTask.Yield();
        }
    }
    private void SetProjectilePosition(Transform projectile, int bulletCount)
    {
        projectile.Rotate(Vector3.forward * _angles[bulletCount]);
        projectile.Translate(projectile.transform.up * DISTANCE_FROM_PLAYER, Space.World);
    }


}
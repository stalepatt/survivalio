using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner
{
    private List<CancellationTokenSource> _waveSpawnTaskCancellationToken;
    private Vector3[] _enemySpawnPoints;

    public void Init()
    {
        _waveSpawnTaskCancellationToken = new List<CancellationTokenSource>();
        _waveSpawnTaskCancellationToken.Add(new CancellationTokenSource());

        SetEnemySpawnPoints();
    }

    private const int ENEMY_SPAWN_POINT_AMOUNT = 36;
    private const int DISTANCE_X_CAMERA_OUT = 5;
    private const int DISTANCE_Y_CAMERA_OUT = 10;
    private void SetEnemySpawnPoints()
    {
        _enemySpawnPoints = new Vector3[ENEMY_SPAWN_POINT_AMOUNT];
        float[] angleDivided = new float[ENEMY_SPAWN_POINT_AMOUNT];
        Utils.GetAngleFromCircleDivide(ENEMY_SPAWN_POINT_AMOUNT, out angleDivided);
        for (int pointId = 0; pointId < ENEMY_SPAWN_POINT_AMOUNT; ++pointId)
        {
            float angle = angleDivided[pointId] * Mathf.Rad2Deg;
            _enemySpawnPoints[pointId] = new Vector3(DISTANCE_X_CAMERA_OUT * Mathf.Cos(angle), DISTANCE_Y_CAMERA_OUT * Mathf.Sin(angle), 0);
        }
    }

    public async UniTaskVoid WaveSpawnTask(WaveInfoData waveData)
    {
        if (_waveSpawnTaskCancellationToken.Count <= waveData.WaveCount)
        {
            _waveSpawnTaskCancellationToken.Add(new CancellationTokenSource());
        }

        if (waveData.Interval == 0)
        {
            foreach (CancellationTokenSource token in _waveSpawnTaskCancellationToken)
            {
                token.Cancel();
            }

            SpawnBoss(waveData.EnemyType);
        }

        int waveTime = (waveData.WaveEnd - waveData.WaveCount) * WaveController.TIME_PER_WAVE;

        while (waveTime > 0)
        {
            for (int i = 0; i < waveData.Amount; ++i)
            {
                SpawnEnemy(waveData.EnemyType);
            }

            await UniTask.Delay(
                delayTimeSpan: TimeSpan.FromSeconds(waveData.Interval),
                cancellationToken: _waveSpawnTaskCancellationToken[waveData.WaveCount].Token);

            waveTime -= waveData.Interval;
        }
    }

    public void Spawn(Define.ItemType itemID)
    {

    }

    public void SpawnEnemy(Define.CharacterType enemyType)
    {
        if (Managers.PoolManager.EnemyPool.Root == null)
        {
            Managers.PoolManager.EnemyPool.Init(new GameObject("EnemyPool"), Managers.PoolManager.ObjectContainer);
        }

        EnemyCharacter newEnemy = Managers.PoolManager.EnemyPool.Get();
        newEnemy.Init(enemyType);
        newEnemy.SetSpawnPosition(_enemySpawnPoints.GetRandomElement());
    }

    public void SpawnBoss(Define.CharacterType enemyType)
    {
        EnemyCharacter boss = Managers.PoolManager.EnemyPool.Get();
        boss.Init(enemyType);
        boss.OnDie -= Managers.GameManager.EndGame;
        boss.OnDie += Managers.GameManager.EndGame;
    }

    public void SpawnDefaultExp()
    {

    }

    public void SpawnExp(int expAmount, Transform position)
    {
        if (Managers.PoolManager.ExpPool.Root == null)
        {
            Managers.PoolManager.ExpPool.Init(new GameObject("ItemPool"), Managers.PoolManager.ObjectContainer);
        }

        Exp expItem = Managers.PoolManager.ExpPool.Get();

        expItem.SetExp(expAmount, position);
    }



    public Spawner()
    {
        Init();
    }
}
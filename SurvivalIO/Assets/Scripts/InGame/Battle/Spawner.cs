using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner
{
    private List<CancellationTokenSource> _waveSpawnTaskCancellationTokenList;
    private Vector3[] _enemySpawnPoints;

    public void Init()
    {
        _waveSpawnTaskCancellationTokenList = new List<CancellationTokenSource>
        {
            new CancellationTokenSource()
        };

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
        if (_waveSpawnTaskCancellationTokenList.Count <= waveData.WaveCount)
        {
            _waveSpawnTaskCancellationTokenList.Add(new CancellationTokenSource());
        }

        int waveTime = (waveData.WaveEnd - waveData.WaveCount) * WaveController.TIME_PER_WAVE;

        if (waveTime < 0)
        {
            await UniTask.WaitUntil(() => Managers.PoolManager.Clear(Managers.PoolManager.EnemyPool.Root));

            SpawnBoss(waveData.EnemyType);
        }

        while (waveTime > 0)
        {
            for (int i = 0; i < waveData.Amount; ++i)
            {
                SpawnEnemy(waveData.EnemyType);
            }

            await UniTask.Delay(
                delayTimeSpan: TimeSpan.FromSeconds(waveData.Interval),
                cancellationToken: _waveSpawnTaskCancellationTokenList[waveData.WaveCount].Token);

            waveTime -= waveData.Interval;
        }

    }

    public void CancelWaveSpawnTask()
    {
        foreach (CancellationTokenSource token in _waveSpawnTaskCancellationTokenList)
        {
            token.Cancel();
        }
        Managers.GameManager.CurrentChapter.CurrentWave.StopWave();
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

    private const int BOSS_SPAWN_OFFSET = 5;

    public void SpawnBoss(Define.CharacterType enemyType)
    {
        Managers.PoolManager.EnemyPool.Init(new GameObject("BossPool"), Managers.PoolManager.ObjectContainer);
        EnemyCharacter boss = Managers.PoolManager.EnemyPool.Get();

        boss.Init(enemyType);
        boss.transform.position = Managers.GameManager.PlayerCharacter.transform.position + Vector3.up * BOSS_SPAWN_OFFSET;

        boss.OnDie -= Managers.GameManager.CurrentChapter.ChapterClear;
        boss.OnDie += Managers.GameManager.CurrentChapter.ChapterClear;
    }

    public void SpawnBoss()
    {
        CancelWaveSpawnTask();
        Managers.PoolManager.Clear(Managers.PoolManager.EnemyPool.Root);

        SpawnBoss(Define.CharacterType.Boss01);
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

    public void Clear()
    {
        foreach (CancellationTokenSource token in _waveSpawnTaskCancellationTokenList)
        {
            if (token.Token != null)
            {
                token.Cancel();
                token.Dispose();
            }
        }
        _waveSpawnTaskCancellationTokenList.Clear();
    }
}
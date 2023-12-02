using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveController
{
    private int _currentWaveCount = 1;
    private List<WaveInfoData> _waveInfos;
    private CancellationTokenSource _waveStopCancellationTokenSource;
    public const int TIME_PER_WAVE = 30;
    public void InitWave(int currentChapter)
    {
        _waveInfos = Managers.DataManager.WaveInfos[currentChapter];
        _waveStopCancellationTokenSource = new CancellationTokenSource();
    }

    public async UniTaskVoid StartWaveTask()
    {
        while (_currentWaveCount < _waveInfos.Count)
        {
            WaveInfoData currentWaveData = _waveInfos[_currentWaveCount];
            Managers.PoolManager.Spawner.WaveSpawnTask(currentWaveData);

            //30초 마다 waveCount 증가, 새로운 wave 진입
            await UniTask.Delay(TimeSpan.FromSeconds(TIME_PER_WAVE), cancellationToken: _waveStopCancellationTokenSource.Token);
            ++_currentWaveCount;
        }
    }

    public void StopWave()
    {

    }

    public WaveController(int ChapterID)
    {
        InitWave(ChapterID);
    }
}
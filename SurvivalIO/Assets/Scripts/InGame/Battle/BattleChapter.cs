using System;
using Unity.VisualScripting;
using UnityEngine;
using Util;
public class BattleChapter
{
    // TO DO : Map 개선
    private ChapterInfoData _chapterInfo;
    private WaveController _currentWave;
    private bool _isClear;
    public BattleData CurrentBattleData { get; private set; }

    public void Init(int currentChapter)
    {
        SetChapterInitialData(currentChapter);
    }

    public void Start()
    {
        // TO DO : 맵 형태에 따른 초기 생성 로직 Init으로 통일
        GameObject mapObject = Managers.ResourceManager.Instantiate("Ingame/Map01");
        foreach (Transform map in mapObject.GetComponentsInChildren<Transform>())
        {
            map.gameObject.GetOrAddComponent<MapController>();
        }

        Managers.PoolManager.Spawner.SpawnDefaultExp();
        _currentWave.StartWaveTask().Forget();
    }
    public void Clear()
    {
        _isClear = true;
        End();
    }
    public void End()
    {
    }
    public void SetChapterInitialData(int chapterID)
    {
        CurrentBattleData = new BattleData(chapterID);
        //_chapterInfo = Managers.DataManager.ChapterInfos[chapterID];
        _currentWave = new WaveController(chapterID);
    }

    public BattleChapter(int chapterID = (int)Define.Chapters.WildStreet)
    {
        Init(chapterID);
    }
}

public class BattleData
{
    public int ChapterID { get; private set; }
    public int KillCount { get; private set; }
    public int GoldEarned { get; private set; }
    public int PlayTime { get; private set; }
    public int PlayTimeToMinute { get { return PlayTime / 60; } }
    public int PlayTimeToSec { get { return PlayTime % 60; } }
    public bool IsClear { get; private set; }

    public void SetData(int chapterID = 0, int killCount = 0, int gold = 0, int time = 0, bool isClear = false)
    {
        KillCount += killCount;
        GoldEarned += gold;
        PlayTime = time;
        if (isClear)
        {
            IsClear = true;
        }
    }

    public BattleData(int chapterID = 0)
    {
        ChapterID = chapterID;
        IsClear = false;
    }
}

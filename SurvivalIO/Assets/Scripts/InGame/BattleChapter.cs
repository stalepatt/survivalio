﻿using System;
using Unity.VisualScripting;
using UnityEngine;
using Util;
public class BattleChapter
{
    // TO DO : Map 개선
    private bool _isClear;
    private Util.Timer IngameTimer;
    private ChapterInfoData ChapterInfo;
    public BattleData CurrentBattleData;

    public void Init()
    {
        IngameTimer = new Util.Timer();
    }

    public void Start()
    {
        // TO DO : SetCurrentChapterMapObject
        // TO DO : Spawner / WaveData 구성 이후 반영
        // Managers.PoolManager.Spawner.SpawnExp();
        if (CurrentBattleData == null)
        {
            new BattleData();
        }
    }
    public void Clear()
    {
        _isClear = true;
        End();
    }
    public void End()
    {
        IngameTimer.Stop();
        CurrentBattleData.SetData(time: IngameTimer.Elapsed.Time);
    }
    public void SetChapterInitialData(int chapterID)
    {
        Debug.Log($"chapterID : {chapterID}");
        //ChapterInfo = Managers.DataManager.ChapterInfos[(int)chapterID];
        CurrentBattleData = new BattleData((int)chapterID);
    }

    public BattleChapter(int chapterID = (int)Define.Chapters.WildStreet)
    {
        Init();
        SetChapterInitialData(chapterID);
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

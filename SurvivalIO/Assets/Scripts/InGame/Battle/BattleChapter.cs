using System;
using UnityEngine;
public class BattleChapter
{
    private ChapterInfoData _chapterInfo; // TO DO : Map 개선에서 반영
    public WaveController CurrentWave { get; private set; }
    public BattleData CurrentBattleData { get; private set; }
    public event Action OnBattleDataChanged;
    public void Init(int currentChapter)
    {
        SetChapterInitialData(currentChapter);
    }

    public void SetChapterInitialData(int chapterID)
    {
        CurrentBattleData = new BattleData(chapterID);
        //_chapterInfo = Managers.DataManager.ChapterInfos[chapterID]; TO DO : 챕터 추가 시 활용
        CurrentWave = new WaveController(chapterID);

        Util.Timer gameTimer = Managers.GameManager.GameTimer;
        gameTimer.OnTimeChanged -= () => SetData(time: gameTimer.Elapsed.Time);
        gameTimer.OnTimeChanged += () => SetData(time: gameTimer.Elapsed.Time);
    }

    public void Start()
    {
        // TO DO : 맵 형태에 따른 초기 생성 로직 Init으로 통일
        GameObject mapObject = Managers.ResourceManager.Instantiate("Ingame/Map01");
        foreach (Transform map in mapObject.GetComponentsInChildren<Transform>())
        {
            map.gameObject.GetOrAddComponent<MapController>();
        }

        Managers.PoolManager.Spawner.Init();
        Managers.PoolManager.Spawner.SpawnDefaultExp();
        CurrentWave.StartWaveTask().Forget();
    }
    public void ChapterClear() => EndChapter(isClear: true);
    public void ChapterFail() => EndChapter(isClear: false);

    private void EndChapter(bool isClear = false)
    {
        CurrentBattleData.SetData(isClear: isClear);

        Managers.GameManager.EndGame();
    }

    public void SetData(int chapterID = 0, int killCount = 0, int gold = 0, int time = 0, bool isClear = false)
    {
        CurrentBattleData.SetData(chapterID, killCount, gold, time, isClear);
        OnBattleDataChanged?.Invoke();
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
        if (time != 0)
        {
            PlayTime = time;
        }
        IsClear = isClear;
    }

    public BattleData(int chapterID = 0)
    {
        ChapterID = chapterID;
        IsClear = false;
    }
}

using System;
using UnityEngine;

public class GameManager
{
    public PlayerCharacter PlayerCharacter { get; set; }

    public BattleChapter CurrentChapter;

    public Util.Timer GameTimer;

    public event Action OnEndGame;

    public int BestTime { get; private set; }
    public void Init()
    {
        GameTimer = new Util.Timer();
    }

    public void StartGame()
    {
        int userSelectedChapter = (int)Define.Chapters.WildStreet; // TO DO : UserData 备己

        CurrentChapter = new BattleChapter(userSelectedChapter);
        CurrentChapter.Init(userSelectedChapter);

        Managers.ResourceManager.Instantiate("Ingame/Player")
            .GetOrAddComponent<PlayerCharacter>()
            .Init();

        Managers.ResourceManager.Instantiate("Ingame/CMFollowCam")
            .GetOrAddComponent<CMFollowCam>()
            .Init(target: PlayerCharacter.transform);

        Managers.UIManager.ShowPopupUI<IngameBattlePopup>();

        CurrentChapter.Start();
        GameTimer.Start();

        OnEndGame -= GameTimer.Clear;
        OnEndGame += GameTimer.Clear;

        //Managers.UIManager.ShowPopupUI<SelectSkillPopup>(); // TO DO : Popup 橇府普 / 胶农赋飘 备己

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ReturnGame()
    {

    }

    public void EndGame()
    {
        OnEndGame?.Invoke();

        Managers.UIManager.ShowPopupUI<IngameResultPopup>().SetInfo(CurrentChapter.CurrentBattleData);
    }

    public bool IsBestTime()
    {
        int currentPlayTime = CurrentChapter.CurrentBattleData.PlayTime;

        if (currentPlayTime > BestTime)
        {
            BestTime = currentPlayTime;
            return true;
        }

        return false;
    }
}

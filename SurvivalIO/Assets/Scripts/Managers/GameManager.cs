using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager
{
    public PlayerCharacter PlayerCharacter { get; set; }

    public BattleChapter CurrentChapter;

    public Util.Timer GameTimer;
    public void Init()
    {
        GameTimer = new Util.Timer();
    }

    public void StartGame()
    {
        int userSelectedChapter = (int)Define.Chapters.WildStreet; // TO DO : UserData ����

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

        //Managers.UIManager.ShowPopupUI<SelectSkillPopup>(); // TO DO : Popup ������ / ��ũ��Ʈ ����

    }

    public void PauseGame()
    {
        Debug.Log("Pause");
    }

    public void ReturnGame()
    {

    }

    public void EndGame()
    {
        CurrentChapter.End();
        Managers.UIManager.ShowPopupUI<IngameResultPopup>().SetInfo(CurrentChapter.CurrentBattleData);
    }
}

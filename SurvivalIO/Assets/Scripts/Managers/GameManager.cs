using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public PlayerCharacter Player { get; set; }

    public BattleChapter CurrentChapter;
    public void Init()
    {

    }

    public void StartGame()
    {
        int userSelectedChapter = (int)Define.Chapters.WildStreet; // TO DO : UserData 구성
        CurrentChapter = new BattleChapter();

        // 맵 생성 // TO DO Chapter로 넘기기
        GameObject mapObject = Managers.ResourceManager.Instantiate("Ingame/Map01");
        foreach (Transform map in mapObject.GetComponentsInChildren<Transform>())
        {
            map.gameObject.GetOrAddComponent<MapController>(); // TO DO : 맵 형태에 따른 초기 생성 로직 Init으로 통일
        }

        Managers.ResourceManager.Instantiate("Ingame/Player")
            .GetOrAddComponent<PlayerCharacter>()
            .Init();

        Managers.ResourceManager.Instantiate("Ingame/CMFollowCam")
            .GetOrAddComponent<CMFollowCam>()
            .Init(target: Player.transform);


        Managers.UIManager.ShowPopupUI<IngamePopup>(); // TO DO : Popup 프리팹 / 스크립트 구성

        // TO DO : 초기 경험치 아이템 보상 생성

        Managers.UIManager.ShowPopupUI<SelectSkillPopup>(); // TO DO : Popup 프리팹 / 스크립트 구성

        CurrentChapter.Start();
    }

    // TO DO : PauseGame

    public void EndGame()
    {
        CurrentChapter.End();
        Managers.UIManager.ShowPopupUI<IngameResultPopup>();
    }
}

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public PlayerCharacter Player { get; set; }
    public void Init()
    {

    }

    public void StartGame()
    {
        // 맵 생성
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

        // 인게임 팝업 생성
        // Managers.UIManager.ShowPopupUI<IngamePopup>(); << TO DO : Time scale 관리를 위한 타임 매니저 필요
        
        // 초기 경험치 아이템 보상 생성

        // 최초 스킬 선택
        // Managers.UIManager.ShowPopupUI<SelectSkillPopup>();

        // 몬스터 웨이브 시작
        // CurrentChapter.Start();
    }

    // TO DO : PauseGame

    public void EndGame()
    {
        // 결과 팝업 표출
        // Managers.UIManager.ShowPopupUI<EndgamePopup>();
    }    
}

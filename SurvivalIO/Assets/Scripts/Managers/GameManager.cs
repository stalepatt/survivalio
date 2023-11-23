using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public PlayerCharacter Player { get; private set; }
    public CMFollowCam FollowCam { get; private set; }

    public void Init()
    {

    }

    public void StartGame()
    {
        // 맵 생성
        GameObject mapObject = Managers.ResourceManager.Instantiate("Ingame/Map01");
        foreach (Transform map in mapObject.GetComponentsInChildren<Transform>())
        {
            map.gameObject.GetOrAddComponent<MapController>();
        }

        // 플레이어 생성
        GameObject playerObject = Managers.ResourceManager.Instantiate("Ingame/Player");
        Player = playerObject.GetOrAddComponent<PlayerCharacter>();
        Player.Init();

        // 팔로우캠 생성
        GameObject followCamObject = Managers.ResourceManager.Instantiate("Ingame/CMFollowCam");
        FollowCam = followCamObject.GetOrAddComponent<CMFollowCam>();
        FollowCam.Init(playerObject);

        // 인게임 팝업 생성
        // 초기 경험치 아이템 생성
        // 최초 스킬 선택
    }
}

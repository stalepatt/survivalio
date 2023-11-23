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
        // �� ����
        GameObject mapObject = Managers.ResourceManager.Instantiate("Ingame/Map01");
        foreach (Transform map in mapObject.GetComponentsInChildren<Transform>())
        {
            map.gameObject.GetOrAddComponent<MapController>();
        }

        // �÷��̾� ����
        GameObject playerObject = Managers.ResourceManager.Instantiate("Ingame/Player");
        Player = playerObject.GetOrAddComponent<PlayerCharacter>();
        Player.Init();

        // �ȷο�ķ ����
        GameObject followCamObject = Managers.ResourceManager.Instantiate("Ingame/CMFollowCam");
        FollowCam = followCamObject.GetOrAddComponent<CMFollowCam>();
        FollowCam.Init(playerObject);

        // �ΰ��� �˾� ����
        // �ʱ� ����ġ ������ ����
        // ���� ��ų ����
    }
}

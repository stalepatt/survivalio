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
        // �� ����
        GameObject mapObject = Managers.ResourceManager.Instantiate("Ingame/Map01");
        foreach (Transform map in mapObject.GetComponentsInChildren<Transform>())
        {
            map.gameObject.GetOrAddComponent<MapController>(); // TO DO : �� ���¿� ���� �ʱ� ���� ���� Init���� ����
        }
                
        Managers.ResourceManager.Instantiate("Ingame/Player")
            .GetOrAddComponent<PlayerCharacter>()
            .Init();
                        
        Managers.ResourceManager.Instantiate("Ingame/CMFollowCam")
            .GetOrAddComponent<CMFollowCam>()
            .Init(target: Player.transform);

        // �ΰ��� �˾� ����
        // Managers.UIManager.ShowPopupUI<IngamePopup>(); << TO DO : Time scale ������ ���� Ÿ�� �Ŵ��� �ʿ�
        
        // �ʱ� ����ġ ������ ���� ����

        // ���� ��ų ����
        // Managers.UIManager.ShowPopupUI<SelectSkillPopup>();

        // ���� ���̺� ����
        // CurrentChapter.Start();
    }

    // TO DO : PauseGame

    public void EndGame()
    {
        // ��� �˾� ǥ��
        // Managers.UIManager.ShowPopupUI<EndgamePopup>();
    }    
}

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
        int userSelectedChapter = (int)Define.Chapters.WildStreet; // TO DO : UserData ����
        CurrentChapter = new BattleChapter();

        // �� ���� // TO DO Chapter�� �ѱ��
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


        Managers.UIManager.ShowPopupUI<IngamePopup>(); // TO DO : Popup ������ / ��ũ��Ʈ ����

        // TO DO : �ʱ� ����ġ ������ ���� ����

        Managers.UIManager.ShowPopupUI<SelectSkillPopup>(); // TO DO : Popup ������ / ��ũ��Ʈ ����

        CurrentChapter.Start();
    }

    // TO DO : PauseGame

    public void EndGame()
    {
        CurrentChapter.End();
        Managers.UIManager.ShowPopupUI<IngameResultPopup>();
    }
}

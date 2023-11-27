using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIBattlePopup : UIPopup
{
    enum Buttons
    {
        GameStartButton
    }

    public override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.GameStartButton).gameObject.BindEvent(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        Managers.SceneManager.ChangeScene(Define.Scene.InGame);
    }
}

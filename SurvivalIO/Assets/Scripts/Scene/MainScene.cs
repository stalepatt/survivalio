using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        SceneType = Define.Scene.Main;

        Managers.UIManager.CloseAllPopupUI();
        Managers.UIManager.ShowPopupUI<MainUICommonPopup>();
        Managers.UIManager.ShowPopupUI<MainUIBattlePopup>();
        
        return true;
    }
}

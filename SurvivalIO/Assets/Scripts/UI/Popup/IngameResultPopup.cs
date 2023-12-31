﻿public class IngameResultPopup : UIPopup
{
    enum Images
    {
        ResultPopupBackground,
        ResultPopupBoxBackground,
        CurrentBattlePlayTimeRecordTextBox,
        ResultPopupTopDeco
    }
    enum Texts
    {
        CurrentBattleChapterText,
        CurrentBattleSuccessText,
        NewBestTimeRecordText,
        NewRecordText,
        BestTimeRecordText,
        CurrentBattleKillCountText,
        ResultPopupTopDecoText,
        CurrentBattleRewardsItemAmountText
    }
    enum Buttons
    {
        ConfirmButton,
        DamageMeterResultButton
    }
    enum Objects
    {
        ResultPopupBox,
        CurrentBattlePlayTimeRecordTextBox,
        CurrentBattleRewardsBox,
        UserBestPlayTimeRecordTextBox,
        NewRecordText,
    }

    public override void Init()
    {
        base.Init();

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));

        GetButton((int)Buttons.ConfirmButton).gameObject.BindEvent(CloseGame);

        Managers.GameManager.PauseGame();
    }

    public void SetInfo(BattleData result)
    {
        GetText((int)Texts.CurrentBattleChapterText).text = $"{result.ChapterID:D2} 챕터";
        GetText((int)Texts.CurrentBattleKillCountText).text = result.KillCount.ToString();

        GetText((int)Texts.CurrentBattleRewardsItemAmountText).text =
            result.GoldEarned >= 1_000 ? $"{result.GoldEarned / 1_000}K" : result.GoldEarned.ToString();

        if (!result.IsClear)
        {
            SetFailurePopup(result);
        }
        else
        {
            SetClearPopup(result);
        }
    }

    public void SetClearPopup(BattleData result)
    {
        GetText((int)Texts.CurrentBattleSuccessText).gameObject.SetActive(true);
        GetText((int)Texts.NewBestTimeRecordText).gameObject.SetActive(false);
        GetText((int)Texts.NewRecordText).gameObject.SetActive(false);

        GetText((int)Texts.ResultPopupTopDecoText).text = "승리";
        GetImage((int)Images.ResultPopupTopDeco).color = Define.RESULT_POPUP_CLEAR_TOP_DECO_COLOR;
        GetImage((int)Images.CurrentBattlePlayTimeRecordTextBox).color = Define.RESULT_POPUP_CLEAR_GLOW_COLOR;

        GetObject((int)Objects.UserBestPlayTimeRecordTextBox).gameObject.SetActive(false);
    }

    public void SetFailurePopup(BattleData result)
    {
        GetText((int)Texts.CurrentBattleSuccessText).gameObject.SetActive(false);
        GetText((int)Texts.NewBestTimeRecordText).gameObject.SetActive(true);
        GetText((int)Texts.NewRecordText).gameObject.SetActive(true);

        GetText((int)Texts.ResultPopupTopDecoText).text = "실패";
        GetImage((int)Images.ResultPopupTopDeco).color = Define.RESULT_POPUP_FAILURE_TOP_DECO_COLOR;
        GetImage((int)Images.CurrentBattlePlayTimeRecordTextBox).color = Define.RESULT_POPUP_FAILURE_GLOW_COLOR;

        GetObject((int)Objects.NewRecordText).gameObject.SetActive(false);

        if (Managers.GameManager.IsBestTime())
        {
            GetObject((int)Objects.NewRecordText).gameObject.SetActive(true);
        }
        int bestTime = Managers.GameManager.BestTime;
        GetText((int)Texts.BestTimeRecordText).text = $"{(bestTime / 60):D2}:{(bestTime % 60):D2}";
        GetText((int)Texts.NewBestTimeRecordText).text = $"{result.PlayTimeToMinute:D2}:{result.PlayTimeToSec:D2}";

    }

    public void CloseGame()
    {
        Managers.PoolManager.Clear();
        Managers.SceneManager.ChangeScene(Define.Scene.Main);
    }
}
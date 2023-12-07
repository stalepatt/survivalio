public class IngameBattlePopup : UIPopup
{
    enum Images
    {
        ExpBarFill
    }

    enum Texts
    {
        TimerText,
        ExpBarLevelText,
        KillCountText,
        CoinCountText
    }

    enum Buttons
    {
        PauseButton
    }

    enum Objects
    {
        CoinCount
    }

    public override void Init()
    {
        base.Init();

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));

        GetText((int)Texts.KillCountText).text = "0";

        GetText((int)Texts.ExpBarLevelText).text = "1";
        GetImage((int)Images.ExpBarFill).fillAmount = 0;

        GetButton((int)Buttons.PauseButton).gameObject
            .BindEvent(Managers.GameManager.PauseGame);

        GetObject((int)Objects.CoinCount)
            .SetActive(false);

        Managers.GameManager.CurrentChapter.OnBattleDataChanged -= RefreshUI;
        Managers.GameManager.CurrentChapter.OnBattleDataChanged += RefreshUI;

        RefreshUI();
    }

    public void RefreshUI()
    {
        SetTimeText();
        SetCountText();
    }
    public void SetTimeText()
    {
        BattleData currentBattleData = Managers.GameManager.CurrentChapter.CurrentBattleData;
        GetText((int)Texts.TimerText).text = $"{currentBattleData.PlayTimeToMinute:D2}:{currentBattleData.PlayTimeToSec:D2}";
    }

    public void SetCountText()
    {
        int killCount = Managers.GameManager.CurrentChapter.CurrentBattleData.KillCount;

        if (killCount > 100_000)
        {
            GetText((int)Texts.KillCountText).text = $"{killCount / 1_000}K";
            return;
        }

        GetText((int)Texts.KillCountText).text = $"{killCount}";
    }

    public void SetExpBar(int currentExp, int maxExp)
    {
        float fillAmount = (float)currentExp / maxExp;
        GetImage((int)Images.ExpBarFill).fillAmount = fillAmount;
    }

    public void SetLevelText(int level)
    {
        GetText((int)Texts.ExpBarLevelText).text = level.ToString();
    }
}
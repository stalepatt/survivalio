using UnityEngine;

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

        Util.Timer currentGameTime = Managers.GameManager.GameTimer;
        SetTimeText(currentGameTime);
        currentGameTime.OnTimeChanged -= () => SetTimeText(currentGameTime);
        currentGameTime.OnTimeChanged += () => SetTimeText(currentGameTime);
    }
    public void SetTimeText(Util.Timer timer)
    {
        GetText((int)Texts.TimerText).text = $"{timer.Elapsed.Min:D2}:{timer.Elapsed.Sec:D2}";
    }

    private int _killCount; // To Do : To the BattleData
    public void SetCountText()
    {
        ++_killCount;
        if (_killCount > 100_000)
        {
            GetText((int)Texts.KillCountText).text = $"{_killCount / 1_000}K";
            return;
        }

        GetText((int)Texts.KillCountText).text = $"{_killCount}";
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
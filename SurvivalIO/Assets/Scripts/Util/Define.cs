using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed
    }

    public enum Scene
    {
        Default,
        Title,
        Main,
        InGame
    }

    public enum SpawnableType
    {
        Default,
        Enemy,
        Item,
        Exp
    }

    public enum CharacterType
    {
        Default,
        Player,
        Enemy01,
        Enemy02,
        Enemy03,
        Boss01
    }

    public enum ItemType
    {
        Default,
        ItemBox,
        LuckyBox,
        Gold,
        Magnet,
        Meat,
        Bomb,
        ExpSmall,
        ExpGreen,
        ExpBlue,
        ExpYellow
    }

    public enum SkillName
    {
        Default,
        Kunai,
        Guardian,
        Drill,
        Football
    }

    public enum Chapters
    {
        Default,
        WildStreet
    }

    public static readonly Color32 RESULT_POPUP_CLEAR_GLOW_COLOR = new Color32(255, 187, 0, 152);
    public static readonly Color32 RESULT_POPUP_FAILURE_GLOW_COLOR = new Color32(0, 94, 255, 152);

    public static readonly Color32 RESULT_POPUP_CLEAR_TOP_DECO_COLOR = new Color32(255, 255, 255, 255);
    public static readonly Color32 RESULT_POPUP_FAILURE_TOP_DECO_COLOR = new Color32(0, 255, 247, 255);

}

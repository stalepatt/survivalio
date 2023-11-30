using System;
public class WaveInfoData : DataManager.ICsvParsable
{
    enum Fields
    {
        ChapterID,
        WaveCount,
        EnemyType,
        Amount,
        Interval,
        WaveEnd
    }


    public int ChapterID { get; set; }
    public int WaveCount { get; set; }
    public Define.CharacterType EnemyType { get; set; }
    public int Amount { get; set; }
    public int Interval { get; set; }
    public int WaveEnd { get; set; }

    public void Parse(DataManager.CsvItem[] row)
    {
        ChapterID = row[(int)Fields.ChapterID].ToInt();
        WaveCount = row[(int)Fields.WaveCount].ToInt();
        EnemyType = (Define.CharacterType)Enum.Parse(typeof(Define.CharacterType), row[(int)Fields.EnemyType].ToString());
        Amount = row[(int)Fields.Amount].ToInt();
        Interval = row[(int)Fields.Interval].ToInt();
        WaveEnd = row[(int)Fields.WaveEnd].ToInt();
    }
}

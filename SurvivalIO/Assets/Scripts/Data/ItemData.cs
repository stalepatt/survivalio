using static Define;

public class ItemData : DataManager.ICsvParsable, DataManager.IKeyOwned<int>
{
    enum Fields
    {
        ID,
        Sprite,
        ExpAmount,
        GoldAmount
    }

    public int ID { get; set; }
    public string Sprite { get; set; }
    public int ExpAmount { get; set; }
    public int GoldAmount { get; set; }

    public int Key => ID;

    public void Parse(DataManager.CsvItem[] row)
    {
        ID = row[(int)Fields.ID].ToInt();
        Sprite = row[(int)Fields.Sprite].ToString();
        ExpAmount = row[(int)Fields.ExpAmount].ToInt();
        GoldAmount = row[(int)Fields.GoldAmount].ToInt();
    }
}
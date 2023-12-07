using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterData : DataManager.ICsvParsable, DataManager.IKeyOwned<Define.CharacterType>
{
    enum Fields
    {
        CharacterID,
        HP,
        Attack,
        Speed,
        Exp,
        Scale,
        DefaultSkill,
        CharacterTypeName,
        Sprite
    }
    public int CharacterID { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
    public int Speed { get; set; }
    public int Exp { get; set; }
    public float Scale { get; set; }
    public string DefaultSkill { get; set; }
    public string CharacterTypeName { get; set; }

    public string Sprite { get; set; }

    public Define.CharacterType Key => (Define.CharacterType)CharacterID;

    public void Parse(DataManager.CsvItem[] row)
    {
        CharacterID = row[(int)Fields.CharacterID].ToInt();
        HP = row[(int)Fields.HP].ToInt();
        Attack = row[(int)Fields.Attack].ToInt();
        Speed = row[(int)Fields.Speed].ToInt();
        Exp = row[(int)Fields.Exp].ToInt();
        Scale = row[(int)Fields.Scale].ToFloat();
        DefaultSkill = row[(int)Fields.DefaultSkill].ToString();
        CharacterTypeName = row[(int)Fields.CharacterTypeName].ToString();
        Sprite = row[(int)Fields.Sprite].ToString();
    }

    public CharacterData Clone()
    {
        return (CharacterData)this.MemberwiseClone();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatData : DataManager.ICsvParsable
{
    enum Fields
    {
        CharacterID,
        Attack,
        Speed,
        Hp,
        DefaultSkill
    }
    public int CharacterID { get; set; }
    public int Attack { get; set; }
    public int Speed { get; set; }
    public int Hp { get; set; }
    public string DefaultSkill { get; set; }

    public void Parse(DataManager.CsvItem[] row)
    {
        CharacterID = row[(int)Fields.CharacterID].ToInt();
        Attack = row[(int)Fields.Attack].ToInt();
        Speed = row[(int)Fields.Speed].ToInt();
        Hp = row[(int)Fields.Hp].ToInt();
        DefaultSkill = row[(int)Fields.DefaultSkill].ToString();
    }

    public CharacterStatData Clone()
    {
        return (CharacterStatData)this.MemberwiseClone();
    }

    public Dictionary<string,Skill> Skill { get; set; }
    private const int MAX_SKILL_COUNT = 12;

    public void SetSkill(string name)
    {
        if (name == null)
        {
            return;
        }

        if (Skill == null)
        {
            Skill = new Dictionary<string, Skill>(MAX_SKILL_COUNT);
        }

        if (false == Skill.ContainsKey(name))
        {
            Skill.Add(name, new Skill(name));
        }
        else
        {
            Skill[name].LevelUp();
        }
    }
}
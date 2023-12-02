using System;

[Serializable]
public class SkillData : DataManager.ICsvParsable
{
    enum Fields
    {
        ID,
        Name,
        NameKOR,
        Level,
        BulletDamage,
        Speed,
        ProjectileCount,
        ProjectileScale,
        ProjectileLifeTime,
        AttackInterval,
        UISprite,
        ProjectileSprite
    }

    public int ID { get; set; }
    public string Name { get; set; }
    public string NameKOR { get; set; }
    public int Level { get; set; }
    public float BulletDamage { get; set; }
    public float Speed { get; set; }
    public int ProjectileCount { get; set; }
    public int ProjectileScale { get; set; }
    public int ProjectileLifeTime { get; set; }
    public int AttackInterval { get; set; }
    public string UISprite { get; set; }
    public string ProjectileSprite { get; set; }

    public void Parse(DataManager.CsvItem[] row)
    {
        ID = row[(int)Fields.ID].ToInt();
        Name = row[(int)Fields.Name].ToString();
        NameKOR = row[(int)Fields.NameKOR].ToString();
        Level = row[(int)Fields.Level].ToInt();
        BulletDamage = row[(int)Fields.BulletDamage].ToFloat();
        Speed = row[(int)Fields.Speed].ToFloat();
        ProjectileCount = row[(int)Fields.ProjectileCount].ToInt();
        ProjectileScale = row[(int)Fields.ProjectileScale].ToInt();
        ProjectileLifeTime = row[(int)Fields.ProjectileLifeTime].ToInt();
        AttackInterval = row[(int)Fields.AttackInterval].ToInt();
        UISprite = row[(int)Fields.UISprite].ToString();
        ProjectileSprite = row[(int)Fields.ProjectileSprite].ToString();
    }
}
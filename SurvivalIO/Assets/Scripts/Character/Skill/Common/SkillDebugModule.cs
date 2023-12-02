using System;
using UnityEngine;
public class SkillDebugModule : MonoBehaviour
{
    public Bullet Bullet;
    public Skill Skill;
    public Data SkillData;
    [Serializable]
    public struct Data
    {
        public int ID;
        public string Name;
        public int Level;
        public float Damage;
        public float Speed;
    }

    public void Start()
    {
        Skill = GetComponent<Skill>();
        SkillData data = Skill.GetCurrentSkillLevelData();
        SkillData.ID = data.ID;
        SkillData.Name = data.Name;
        SkillData.Level = data.Level;
        SkillData.Damage = data.BulletDamage;
        SkillData.Speed = data.Speed;
    }

    public void Update()
    {

    }
}
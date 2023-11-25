using System.Collections.Generic;
using UnityEngine;

public class CharacterDebugModule : MonoBehaviour
{
    public CharacterBase Character;

    public int Speed;
    public List<string> SkillList;

    public void Awake()
    {
        Character = Utils.GetOrAddComponent<CharacterBase>(gameObject);

        Speed = Character.Stat.Speed;

        SkillList = new List<string>();
               
    }

    public void Start()
    {
        foreach (string skillName in Character.Stat.Skill.Keys)
        {            
            SkillList.Add(skillName);
        }
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void OnDisable()
    {
        
    }
}
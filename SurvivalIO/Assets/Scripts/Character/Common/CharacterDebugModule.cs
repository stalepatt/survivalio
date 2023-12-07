using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDebugModule : MonoBehaviour
{
    public CharacterBase Character;

    public int Speed;

    public string Name;

    public List<string> SkillList;

    public void Awake()
    {

    }

    public void Start()
    {
        Character = Utils.GetOrAddComponent<CharacterBase>(gameObject);

        Speed = Character.Stat.Speed;

        Name = Character.Stat.CharacterTypeName;

        SkillList = new List<string>();
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void OnDisable()
    {
        Character.TakeDamage(500);
    }
}
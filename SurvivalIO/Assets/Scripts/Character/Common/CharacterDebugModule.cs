using System.Collections.Generic;
using System.Threading;
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
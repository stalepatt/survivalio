using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SciptableObject/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("StatInfo")]
    public int Hp;
    public int Attack;
    public int Defense;
    public int Speed;

    [Header("PlayInfo")]
    public int Gold;
    public Define.Chapters SelectChapter;
    public List<int> PlayTime; //index = chapter
}
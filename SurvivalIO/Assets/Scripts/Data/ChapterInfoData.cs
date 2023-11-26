using System;
using System.Collections.Generic;
using UnityEngine;

public class ChapterInfoData
{
    public int ChapterID;

    public string MapSprite;
    public string MapType;

    
    public ChapterInfoData Clone()
    {
        return (ChapterInfoData)this.MemberwiseClone();
    }
}
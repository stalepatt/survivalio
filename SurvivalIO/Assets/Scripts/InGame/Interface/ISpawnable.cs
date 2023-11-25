using System;
using UnityEngine;
public interface ISpawnable
{
    Define.SpawnableType GetSpawnableObjectType();
    void Spawn();
    void Return();
}
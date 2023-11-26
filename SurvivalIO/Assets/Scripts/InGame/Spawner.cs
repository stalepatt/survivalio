using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner
{
    public void Spawn(Define.SpawnableType spawnable, Define.CharacterType enemyID = Define.CharacterType.Default, Define.ItemType itemID = Define.ItemType.Default)
    {
        switch (spawnable)
        {
            case Define.SpawnableType.Enemy:
                SpawnEnemy(enemyID);
                break;

            case Define.SpawnableType.Item:
                SpawnItem(itemID);
                break;

            case Define.SpawnableType.Exp:
                SpawnExp();
                break;

            case Define.SpawnableType.Default:

                return;
        }
    }

    public void SpawnEnemy(Define.CharacterType enemyID)
    {

    }

    public void SpawnItem(Define.ItemType itemID)
    {

    }

    public void SpawnExp()
    {

    }

    public void SpawnBullet()
    {

    }

    public void StartWave(int chapterID)
    {
        // GetWaveData(chpterID)        
    }
}
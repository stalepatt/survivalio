using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner
{
    private GameObject ObjectContainer;
    private Pool<EnemyCharacter> Enemy;

    public void Init()
    {
        Managers.ResourceManager.Destroy(ObjectContainer);

        ObjectContainer = new GameObject("Object Containers");
        Enemy = new Pool<EnemyCharacter>();
        Enemy.Init(ObjectContainer);
    }


}
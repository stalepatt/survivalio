using UnityEngine;

public class PoolManager
{
    private GameObject ObjectContainer;
    private Pool<EnemyCharacter> EnemyPool;
    public Spawner Spawner;
    public void Init()
    {
        Managers.ResourceManager.Destroy(ObjectContainer);

        ObjectContainer = new GameObject("Object Containers");
        EnemyPool = new Pool<EnemyCharacter>();
        EnemyPool.Init(ObjectContainer);
    }

    private void Spawn()
    {

    }
}
using UnityEngine;

public class PoolManager
{
    public GameObject ObjectContainer { get; private set; }
    public Pool<EnemyCharacter> EnemyPool;
    public Pool<Bullet> BulletPool;
    public Spawner Spawner;
    public void Init()
    {
        Spawner = new Spawner();

        Managers.ResourceManager.Destroy(ObjectContainer);
        ObjectContainer = new GameObject("@ObjectContainers");
        ObjectContainer.transform.SetParent(Managers.Instance.transform);

        EnemyPool = new Pool<EnemyCharacter>();
        BulletPool = new Pool<Bullet>();
    }

    public void Clear()
    {
        foreach (Transform child in ObjectContainer.transform)
        {
            Managers.ResourceManager.Destroy(child.gameObject);
        }
    }
}
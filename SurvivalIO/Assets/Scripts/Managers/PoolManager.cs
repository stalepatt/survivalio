using UnityEngine;

public class PoolManager
{
    public GameObject ObjectContainer { get; private set; }
    public Pool<EnemyCharacter> EnemyPool { get; private set; }
    public Pool<Bullet> BulletPool { get; private set; }
    public Pool<Exp> ExpPool { get; private set; }
    public Spawner Spawner { get; private set; }
    public void Init()
    {
        Managers.ResourceManager.Destroy(ObjectContainer);
        ObjectContainer = new GameObject("@ObjectContainers");
        ObjectContainer.transform.SetParent(Managers.Instance.transform);

        EnemyPool = new Pool<EnemyCharacter>();
        BulletPool = new Pool<Bullet>();
        ExpPool = new Pool<Exp>();
        Spawner = new Spawner();
    }

    public bool Clear(GameObject pool = null)
    {
        Transform targetPool = ObjectContainer.transform;

        if (pool != null)
        {
            targetPool = pool.transform;
        }

        foreach (Transform child in targetPool)
        {
            Managers.ResourceManager.Destroy(child.gameObject);
        }

        Spawner.Clear();

        return true;
    }
}
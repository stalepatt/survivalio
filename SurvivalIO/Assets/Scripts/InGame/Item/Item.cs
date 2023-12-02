using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected PlayerCharacter _player;
    public virtual void Init(ItemData data)
    {
        SpriteRenderer renderer = Utils.GetOrAddComponent<SpriteRenderer>(gameObject);
        Sprite itemSprite = Managers.ResourceManager.Load<Sprite>(data.Sprite);
        renderer.sprite = itemSprite;
        renderer.sortingOrder = 3;

        _player = Managers.GameManager.PlayerCharacter;
    }

    public abstract void Effect();
    public abstract void Return();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Effect();
        Return();
    }
}

// TO DO : 개별 컴포넌트화
//public class Meat : Item
//{
//    private const float DEFAULT_HEAL_AMOUNT = 0.5f;

//    public override void Effect()
//    {
//        _player.Heal(DEFAULT_HEAL_AMOUNT);
//    }
//}

//public class Gold : Item
//{
//    public override void Effect()
//    {
//        throw new System.NotImplementedException();
//    }
//}

//public class Magnet : Item
//{
//    public override void Effect()
//    {
//        IMagnetic[] magneticItems = GetMagneticItems();

//        foreach (IMagnetic item in magneticItems)
//        {
//            item.Magnetize();
//        }

//        //static IMagnetic[] GetMagneticItems()
//        //{
//        //    GameObject itemPool = Managers.PoolManager.ExpPool.Root;
//        //    IMagnetic[] magneticItems = itemPool.GetComponentsInChildren<IMagnetic>();

//        //    return magneticItems;
//        //}
//    }
//}

//public class Bomb : Item
//{
//    private const int BOMB_DAMAGE = 10;
//    public override void Effect()
//    {
//        GameObject enemyPool = Managers.PoolManager.EnemyPool.Root;

//        foreach (Transform enemy in enemyPool.transform)
//        {
//            enemy.GetComponent<EnemyCharacter>().TakeDamage(BOMB_DAMAGE);
//        }
//    }
//}

using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private float _speed;
    private PlayerCharacter _player;


    public void Init(Transform skill, SkillData data)
    {
        this.transform.parent = skill.transform;
        this.transform.localScale *= data.ProjectileScale;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        _player = Managers.GameManager.PlayerCharacter;
        _speed = data.Speed;
        _damage = Mathf.FloorToInt(data.BulletDamage * _player.Stat.Attack);

        Sprite spriteResource = Managers.ResourceManager.Load<Sprite>(data.ProjectileSprite);
        SpriteRenderer renderer = Utils.GetOrAddComponent<SpriteRenderer>(gameObject);
        renderer.sprite = spriteResource;
        renderer.sortingOrder = 6;


        if (data.ProjectileLifeTime != 0)
        {
            StartProjectileLifeTimeTask(data.ProjectileLifeTime).Forget();
        }
    }

    public void Return()
    {
        Managers.PoolManager.BulletPool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagableObject = collision.gameObject.GetComponent<IDamagable>();
        if (damagableObject == null)
        {
            return;
        }

        damagableObject.TakeDamage(_damage);        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Sensor"))
        {
            Managers.PoolManager.BulletPool.Release(this);
        }
    }

    private async UniTaskVoid StartProjectileLifeTimeTask(int lifeTime)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(lifeTime));
        if (this.gameObject.activeSelf)
        {
            Managers.PoolManager.BulletPool.Release(this);
        }
    }

}
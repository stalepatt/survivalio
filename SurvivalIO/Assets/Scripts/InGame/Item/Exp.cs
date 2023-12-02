using UnityEngine;
public class Exp : Item, IMagnetic
{
    private int _expAmount;
    private readonly Vector3 AMOUNT_ONE_SCALE = new Vector3(0.2f, 0.2f, 1);
    public void SetExp(int amount, Transform enemy)
    {
        transform.position = enemy.position;
        if (amount == 1)
        {
            transform.localScale = AMOUNT_ONE_SCALE;
        }

        _expAmount = amount;

        ItemData expData = Managers.DataManager.ItemDatas[GetExpIndex(amount)];
        base.Init(expData);

        static int GetExpIndex(int amount)
        {
            switch (amount)
            {
                case 1: return 1;
                case 5: return 2;
                case 10: return 3;
                case 50: return 4;
                default: return 0;
            }
        }
    }

    public override void Effect()
    {
        _player.GetExp(_expAmount);
    }

    public override void Return()
    {
        Managers.PoolManager.ExpPool.Release(this);
    }

    public void Magnetize()
    {
        transform.Translate(_player.transform.position * Time.deltaTime);
    }
}
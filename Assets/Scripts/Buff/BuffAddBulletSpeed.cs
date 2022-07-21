
using UnityEngine;
/// <summary>
/// 子弹增加30%的速度
/// </summary>
public class BuffAddBulletSpeed : Buff<Bullet>
{
    public override bool AddBuff(Bullet target)
    {

        if (count < maxCount)
        {
            count++;
            target.speed = target.initSpeed * (1 + 0.3f * count);
            return true;
        }
        return false;
    }

    public override void Clear(Bullet target)
    {
        count = 0;
        target.speed = target.initSpeed;
    }
}
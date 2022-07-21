/// <summary>
/// 子弹增加10%的几率冰冻敌人一秒
/// </summary>
public class BuffBulletfrozen : Buff<Bullet>
{
    public override bool AddBuff(Bullet target)
    {
        if (count < maxCount)
        {
            count++;
            target.frozen = 1 - 1 / (0.1f * count + 1);
            return true;
        }
        return false;
    }
    public override void Clear(Bullet target)
    {
        count = 0;
        target.frozen = 0;
    }
}
using UnityEngine;

public abstract class Buff<T>
{
    public int maxCount;
    public int count;
    public abstract bool AddBuff(T target);
    public abstract void Clear(T target);
}
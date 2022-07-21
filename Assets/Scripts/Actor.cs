using System;
using System.Diagnostics;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected float maxHp;
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float moveSpeed;
    /// <summary>
    /// 是否正在攻击
    /// </summary>
    [SerializeField]
    public bool isAttack;

    /// <summary>
    /// 获取血量
    /// </summary>
    public float Hp
    {
        get => hp;
        protected set
        {
            if (value < 0)
                hp = 0;
            else
                hp = value;
        }
    }
    /// <summary>
    /// 最大血量
    /// </summary>
    public float MaxHp { get => maxHp; }
    protected float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害量</param>
    public virtual void Hurt(float damage)
    {
        Hp -= damage;
        if (Hp == 0)
            Death();
    }

    public virtual void Death()
    {
    }
}

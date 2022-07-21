using UnityEngine;

/// <summary>
/// 子弹
/// </summary>
public class Bullet : MonoBehaviour
{
    #region 子弹属性
    /// <summary>
    /// 子弹速度
    /// </summary>
    public float speed = 15;
    [Header("子弹基础速度")]
    public float initSpeed = 15;

    /// <summary>
    /// 子弹基础伤害
    /// </summary>
    public float damage = 1;
    [Header("子弹基础伤害")]
    private float initDamage = 1;

    /// <summary>
    /// 子弹暴击几率（伤害200%）
    /// </summary>
    private float damageDouble;

    /// <summary>
    /// 子弹是否暴击
    /// </summary>
    private bool isDamageDouble;

    /// <summary>
    /// 伤害增幅（百分比）
    /// </summary>
    private float damagePercentage;

    /// <summary>
    /// 伤害增加（具体数值）
    /// </summary>
    private float damageAdd;
    /// <summary>
    /// 自动跟踪
    /// </summary>
    private bool autoTracking;
    /// <summary>
    /// 自动跟踪的目标
    /// </summary>
    private Monster autoTarget;
    /// <summary>
    /// 冰冻几率
    /// </summary>
    public float frozen;
    /// <summary>
    /// 燃烧
    /// </summary>
    private bool combustion;
    /// <summary>
    /// 穿透次数
    /// </summary>
    private int penetrate;
    /// <summary>
    /// 反弹次数
    /// </summary>
    private int rebound;



    #endregion

    #region 属性方法
    /// 1－（1／x*n＋1）
    #endregion
    /// <summary>
    /// 子弹2D刚体
    /// </summary>
    private Rigidbody2D bulletRigidbody;
    /// <summary>
    /// 子弹击中时的爆炸
    /// </summary>
    [SerializeField, Tooltip("子弹击中的爆炸效果")]
    private GameObject BulletEffect;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// 伤害计算
    /// </summary>
    /// <returns>子弹伤害</returns>
    public float GetAttack()
    {
        //(基础伤害+增加伤害)*子弹暴击（200%）*伤害增幅
        return (damage + damageAdd) * (isDamageDouble ? 2 : 1) * (1 + damagePercentage / 100);
    }

    /// <summary>
    /// 设置子弹方向
    /// </summary>
    /// <param name="direction">射击方向</param>
    public void SetDirection(Vector2 direction)
    {
        bulletRigidbody.velocity = direction * speed;
    }
    /// <summary>
    /// 子弹击中
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            //子弹击中特效
            GameObject effect = ObjectPool.Instance.GetObject(BulletEffect);
            effect.transform.position = transform.position;

            //把子弹加入池子
            ObjectPool.Instance.PushObject(gameObject);


            if (collision.tag == "Monster")
            {
                //子弹造成伤害
                collision.transform.GetComponent<Monster>().Hurt(GetAttack());
                //子弹的击退效果
                Vector2 repel = collision.transform.position - transform.position;
                repel.Normalize();
                collision.transform.position = new Vector2(collision.transform.position.x + repel.x / 2, collision.transform.position.y + repel.y / 2);
            }
        }
    }
}
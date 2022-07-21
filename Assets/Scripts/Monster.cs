using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 怪物
/// </summary>
public class Monster : Actor
{

    /// <summary>
    /// 攻击速度
    /// </summary>
    protected float attackSpeed;
    /// <summary>
    /// 受伤闪动持续时间
    /// </summary>
    private float hurtTime;
    /// <summary>
    /// 受伤闪动的计数器
    /// </summary>
    private float hurtValue;

    // [SerializeField]
    private Transform player;
    private Animator animator;
    private SpriteRenderer sr;
    NavMeshAgent navMeshAgent;

    protected new float MoveSpeed
    {
        get => moveSpeed; set
        {
            moveSpeed = value;
            navMeshAgent.speed = moveSpeed;
        }
    }

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    protected void Start()
    {

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
    public void OnEnable()
    {
        Hp = MaxHp;
        hurtTime = 0.1f;
        isAttack = false;
    }
    protected void Update()
    {
        HurtShader();
        if (GameManager.Instance.gameState == GameState.enGaming && !isAttack)
        {
            Move();
        }

    }
    protected void Move()
    {
        //怪物跟随玩家
        Vector3 direction = player.position - transform.position;
        //Debug.DrawRay(transform.position, direction, Color.green);
        direction.Normalize();
        // transform.Translate(direction * Time.deltaTime * monster.MoveSpeed, Space.World);
        //通过NavMeshPlus进行怪物AI寻路
        navMeshAgent.SetDestination(player.position);

        if (player.position.x > transform.position.x)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttack && collision.tag == "Player")
        {
            Attack();
            collision.transform.GetComponent<Player>().Hurt(damage);
        }


    }

    protected void HurtShader()
    {
        if (hurtValue <= 0)
            sr.material.SetFloat("_FlashAmount", 0);
        else
            hurtValue -= Time.deltaTime;
    }
    protected void Attack()
    {
        isAttack = true;
        animator.SetTrigger("isAttack");
        StartCoroutine(FinishAttack());
    }

    private IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(attackSpeed);
        isAttack = false;
    }
    public override void Hurt(float damage)
    {
        base.Hurt(damage);
        sr.material.SetFloat("_FlashAmount", 0.5f);
        hurtValue = hurtTime;
    }

    public override void Death()
    {
        ObjectPool.Instance.PushObject(gameObject);

    }
}

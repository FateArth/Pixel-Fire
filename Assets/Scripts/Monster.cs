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
    private float _attackSpeed;
    /// <summary>
    /// 受伤闪动持续时间
    /// </summary>
    private float _hurtTime;
    /// <summary>
    /// 受伤闪动的计数器
    /// </summary>
    private float _hurtValue;

    // [SerializeField]
    private Transform _player;
    private Animator _animator;
    private SpriteRenderer _sr;
    private NavMeshAgent _navMeshAgent;

    protected new float MoveSpeed
    {
        get => moveSpeed; set
        {
            moveSpeed = value;
            _navMeshAgent.speed = moveSpeed;
        }
    }

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _sr = GetComponent<SpriteRenderer>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    protected void Start()
    {

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }
    public void OnEnable()
    {
        Hp = MaxHp;
        _hurtTime = 0.1f;
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
        Vector3 direction = _player.position - transform.position;
        //Debug.DrawRay(transform.position, direction, Color.green);
        direction.Normalize();
        // transform.Translate(direction * Time.deltaTime * monster.MoveSpeed, Space.World);
        //通过NavMeshPlus进行怪物AI寻路
        _navMeshAgent.SetDestination(_player.position);

        if (_player.position.x > transform.position.x)
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
        if (_hurtValue <= 0)
            _sr.material.SetFloat("_FlashAmount", 0);
        else
            _hurtValue -= Time.deltaTime;
    }
    protected void Attack()
    {
        isAttack = true;
        _animator.SetTrigger("isAttack");
        StartCoroutine(FinishAttack());
    }

    private IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(_attackSpeed);
        isAttack = false;
    }
    public override void Hurt(float damage)
    {
        base.Hurt(damage);
        _sr.material.SetFloat("_FlashAmount", 0.5f);
        _hurtValue = _hurtTime;
    }

    public override void Death()
    {
        ObjectPool.Instance.PushObject(gameObject);

    }
}


using System.Collections;
using UnityEngine;

public class Player : Actor
{
    [TextArea(1,3)]
    public string[] a;
    private Vector2 _mousePos;
    private Rigidbody2D _playerRigidboy;
    private Animator _animator;
    private Vector2 _move;
    
    private void Awake()
    {
        _playerRigidboy = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Init();
        GameManager.Instance.player = this;

    }
    public void Init()
    {
        Hp = MaxHp;
        transform.position = Vector2.zero;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.enGaming)
        {
            Move();
        }
    }

    public override void Hurt(float damage)
    {
        _animator.SetTrigger("isHurt");
        UIManager.Instance.PlayerHeart();
        CameraManager.Instance.ShakeFor(0.1f, 1f);
        base.Hurt(damage);
    }
    public override void Death()
    {
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }

    private void Move()
    {
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.y = Input.GetAxisRaw("Vertical");
        _playerRigidboy.velocity = _move.normalized * MoveSpeed;
        //人物跟随鼠标转向
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_mousePos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (_move != Vector2.zero)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }
}


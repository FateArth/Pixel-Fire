
using System.Collections;
using UnityEngine;

public class Player : Actor
{
    [TextArea(1,3)]
    public string[] a;
    private Vector2 mousePos;
    private Rigidbody2D playerRigidboy;
    private Animator animator;
    private Vector2 move;
    private void Awake()
    {
        playerRigidboy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("isHurt");
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
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        playerRigidboy.velocity = move.normalized * MoveSpeed;
        //人物跟随鼠标转向
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (move != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}


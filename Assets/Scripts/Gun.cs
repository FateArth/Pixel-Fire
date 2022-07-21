using UnityEngine;

/// <summary>
/// 枪
/// </summary>
public class Gun : MonoBehaviour
{
    /// <summary>
    /// 子弹偏转角度
    /// 由扩散范围随机而来
    /// </summary>
    private float angle;

    /// <summary>
    /// 发射扩散范围，随机后生成具体的子弹偏转角度
    /// </summary>
    private float diffuse;
    private float speed;
     /// <summary>
    /// 枪的方向
    /// </summary>
    private Vector2 direction;
    /// <summary>
    /// 枪口，子弹点
    /// </summary>
    private Transform muzzle;
    /// <summary>
    /// 子弹预制体
    /// </summary>
    public GameObject bulletPrefab;
    public Gun()
    {
        diffuse = 10;
        speed = 0.5f;
    }
    /// <summary>
    /// 开枪间隔
    /// </summary>
    public float Speed { get => speed; set => speed = value; }

    /// <summary>
    /// 获取枪发射子弹的偏转角度
    /// </summary>
    /// <returns>随机的偏转角度</returns>
    public float GetAngle()
    {
        angle = Random.Range(-(diffuse / 2), diffuse / 2);

        return angle;

    }
    private float fireTime;
   
    private void Awake()
    {
        muzzle = transform.Find("Muzzle");
    }
    void Start()
    {
        fireTime = speed;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState == GameState.enGaming)
        {
            Shoot();
        }
    }
    /// <summary>
    /// 射击
    /// </summary>
    private void Shoot()
    {
        direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;
        if (Input.GetButton("Fire1"))
        {
            fireTime += Time.deltaTime;
            if (fireTime - speed > 0)
            {
                Fire();
                fireTime = 0;
            }

        }
        if (Input.GetButtonUp("Fire1"))
        {
            fireTime = speed;
        }
    }

    /// <summary>
    /// 发射子弹
    /// </summary>
    private void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;

        Bullet bulletObj = bullet.GetComponent<Bullet>();
        bulletObj.SetDirection(Quaternion.AngleAxis(GetAngle(), Vector3.forward) * direction);
    }
}
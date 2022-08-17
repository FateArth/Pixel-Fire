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
    private float _angle;

    /// <summary>
    /// 发射扩散范围，随机后生成具体的子弹偏转角度
    /// </summary>
    private float _diffuse;
    private float _speed;
     /// <summary>
    /// 枪的方向
    /// </summary>
    private Vector2 _direction;
    /// <summary>
    /// 枪口，子弹点
    /// </summary>
    private Transform _muzzle;
    /// <summary>
    /// 子弹预制体
    /// </summary>
    public GameObject bulletPrefab;
    public Gun()
    {
        _diffuse = 10;
        _speed = 0.5f;
    }
    /// <summary>
    /// 开枪间隔
    /// </summary>
    public float Speed { get => _speed; set => _speed = value; }

    /// <summary>
    /// 获取枪发射子弹的偏转角度
    /// </summary>
    /// <returns>随机的偏转角度</returns>
    private float GetAngle()
    {
        _angle = Random.Range(-(_diffuse / 2), _diffuse / 2);

        return _angle;

    }
    private float _fireTime;
   
    private void Awake()
    {
        _muzzle = transform.Find("Muzzle");
    }
    void Start()
    {
        _fireTime = _speed;
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
        _direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = _direction;
        if (Input.GetButton("Fire1"))
        {
            _fireTime += Time.deltaTime;
            if (_fireTime - _speed > 0)
            {
                Fire();
                _fireTime = 0;
            }

        }
        if (Input.GetButtonUp("Fire1"))
        {
            _fireTime = _speed;
        }
    }

    /// <summary>
    /// 发射子弹
    /// </summary>
    private void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = _muzzle.position;
        bullet.transform.rotation = _muzzle.rotation;

        Bullet bulletObj = bullet.GetComponent<Bullet>();
        bulletObj.SetDirection(Quaternion.AngleAxis(GetAngle(), Vector3.forward) * _direction);
    }
}
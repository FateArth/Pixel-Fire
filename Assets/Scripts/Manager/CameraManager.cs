using System.Collections;
using UnityEngine;
/// <summary>
/// 摄像机处理
/// </summary>
public class CameraManager : Singleton<CameraManager>
{
    private Transform playerTransform;
    public bool startShake = false;  //camera是否开始震动
    public float seconds = 0f;    //震动持续秒数
    public bool started = false;    //是否已经开始震动
    public float quake = 0.2f;       //震动系数
    protected override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        CameraFollowPlayer();
    }
    void LateUpdate()
    {
        if (startShake)
        {
            transform.localPosition = transform.localPosition + Random.insideUnitSphere * quake;
        }

        if (started)
        {
            StartCoroutine(WaitForSecond(seconds));
            started = false;
        }
    }
    /// <summary>
    /// 外部调用控制camera震动
    /// </summary>
    /// <param name="a">震动时间</param>
    /// <param name="b">震动幅度</param>
    public void ShakeFor(float a, float b)
    {

        seconds = a;
        started = true;
        startShake = true;
        quake = b;
    }
    IEnumerator WaitForSecond(float a)
    {
        yield return new WaitForSeconds(a);
        startShake = false;
    }
    void CameraFollowPlayer()
    {
        transform.localPosition = new Vector3(playerTransform.localPosition.x, playerTransform.localPosition.y, transform.localPosition.z);
    }
}

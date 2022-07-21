using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹特效
/// </summary>
public class BulletEffect : MonoBehaviour
{
    private Animator effectAnimator;
    private AnimatorStateInfo info;
    void Awake()
    {
        effectAnimator=GetComponent<Animator>();
    }

    void Update()
    {
        info = effectAnimator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}

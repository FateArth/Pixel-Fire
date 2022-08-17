using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹特效
/// </summary>
public class BulletEffect : MonoBehaviour
{
    private Animator _effectAnimator;
    private AnimatorStateInfo _info;
    void Awake()
    {
        _effectAnimator=GetComponent<Animator>();
    }

    void Update()
    {
        _info = _effectAnimator.GetCurrentAnimatorStateInfo(0);
        if (_info.normalizedTime >= 1)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}

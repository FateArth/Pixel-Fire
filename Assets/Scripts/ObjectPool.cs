using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池
/// </summary>
public class ObjectPool:Singleton<ObjectPool>
{
    private GameObject _pool;
    private readonly Dictionary<string, Queue<GameObject>> _objectPool = new Dictionary<string, Queue<GameObject>>();

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 通过预制体获取到物体对象
    /// 如果对象池里有就返回此物体，否则创建一个放进对象池再返回
    /// </summary>
    /// <param name="prefab">预制体</param>
    /// <returns>物体对象</returns>
    public GameObject GetObject(GameObject prefab)
    {
        GameObject obj;
        if (!_objectPool.ContainsKey(prefab.name) || _objectPool[prefab.name].Count == 0)
        {
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(transform);
            }
            obj = GameObject.Instantiate(prefab, childPool.transform, true);
            PushObject(obj);
        }
        obj = _objectPool[prefab.name].Dequeue();
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 把物体放入对象池并隐藏
    /// </summary>
    /// <param name="prefab">预制体</param>
    public void PushObject(GameObject prefab)
    {
        string str = prefab.name.Replace("(Clone)", string.Empty);
        if (!_objectPool.ContainsKey(str))
        {
            _objectPool.Add(str, new Queue<GameObject>());
        }
        _objectPool[str].Enqueue(prefab);
        prefab.SetActive(false);
    }
}
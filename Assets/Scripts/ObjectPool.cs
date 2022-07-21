using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池
/// </summary>
public class ObjectPool:Singleton<ObjectPool>
{
    private GameObject pool;
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    /// <summary>
    /// 通过预制体获取到物体对象
    /// 如果对象池里有就返回此物体，否则创建一个放进对象池再返回
    /// </summary>
    /// <param name="prefab">预制体</param>
    /// <returns>物体对象</returns>
    public GameObject GetObject(GameObject prefab)
    {
        GameObject _obj;
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            _obj = GameObject.Instantiate(prefab);
            PushObject(_obj);
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(transform);
            }
            _obj.transform.SetParent(childPool.transform);
        }
        _obj = objectPool[prefab.name].Dequeue();
        _obj.SetActive(true);
        return _obj;
    }

    /// <summary>
    /// 把物体放入对象池并隐藏
    /// </summary>
    /// <param name="prefab">预制体</param>
    public void PushObject(GameObject prefab)
    {
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        if (!objectPool.ContainsKey(_name))
        {
            objectPool.Add(_name, new Queue<GameObject>());
        }
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }
}
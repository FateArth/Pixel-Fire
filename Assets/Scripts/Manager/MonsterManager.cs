using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MonsterManager : Singleton<MonsterManager>
{
    public GameObject monsterPrefab;
    private Transform[] monsterPoint;
    private List<Coroutine> coroutineList = new List<Coroutine>();
    private void Start()
    {
        monsterPoint = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            monsterPoint[i] = transform.GetChild(i);
        }
    }
    void Update()
    {
        if (GameManager.Instance.gameState != GameState.enGaming)
        {
            for (int i = 0 ; i <coroutineList.Count ; i++)
            {
                StopCoroutine(coroutineList[i]);
            }
        }
    }
    public void StartSpawn()
    {
        switch (LevelManager.Instance.nowLevel)
        {
            case 1:
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 0, monsterPrefab, 10, 3)));
                break;

            case 2:
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 0, monsterPrefab, 30, 3)));
                break;

            case 3:
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 0, monsterPrefab, 60, 2)));
                break;
            case 4:
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 0, monsterPrefab, 50, 3)));
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 1, monsterPrefab, 50, 3)));
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 3, monsterPrefab, 50, 3)));
                break;
            case 5:
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 0, monsterPrefab, 100, 2)));
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 1, monsterPrefab, 100, 2)));
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 2, monsterPrefab, 100, 2)));
                coroutineList.Add(StartCoroutine(IRepeatSpawn(Spawn, 3, monsterPrefab, 100, 2)));
                break;

            default:
                break;
        }
    }
    /// <summary>
    /// 协程生成怪物
    /// </summary>
    /// <param name="action">生成怪物的方法</param>
    /// <param name="monsterIndex">怪物出生点</param>
    /// <param name="monster">怪物对象</param>
    /// <param name="count">生成的怪物数量</param>
    /// <param name="duration">生成间隔</param>
    /// <returns></returns>
    private static IEnumerator IRepeatSpawn(Action<int> action, int monsterIndex, GameObject monster, int count, float duration)
    {
        for (var i = 0; i < count;)
        {

            action(monsterIndex);
            i++;
            yield return new WaitForSeconds(duration);
        }
    }

    /// <summary>
    /// 生成怪物
    /// </summary>
    private void Spawn(int monsterIndex)
    {
        GameObject monster = ObjectPool.Instance.GetObject(monsterPrefab);
        monster.transform.position = monsterPoint[monsterIndex].position;
    }
}
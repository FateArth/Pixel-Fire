
using System;

public class LevelManager : Singleton<LevelManager>
{
    /// <summary>
    /// 当前关卡
    /// </summary>
    public int nowLevel;
    /// <summary>
    /// 最大关卡
    /// </summary>
    public int maxLevel;
    /// <summary>
    /// 是否进行下一关
    /// </summary>
    public bool nextLevel;

    internal void Init()
    {
        nowLevel = 1;
        MonsterManager.Instance.StartSpawn();

    }
    public void NextLevel()
    {
        nextLevel = true;
        nowLevel++;
        MonsterManager.Instance.StartSpawn();
    }
}

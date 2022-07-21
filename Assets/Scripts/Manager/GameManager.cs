using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    enGameWait,
    enGaming
}
public class GameManager : Singleton<GameManager>
{
    public Player player;

    public GameState gameState;
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }
    internal void GameStart()
    {
        gameState = GameState.enGaming;
        LevelManager.Instance.Init();
    }
    public void GameOver()
    {
        gameState = GameState.enGameWait;
        UIManager.Instance.GameOver();
    }
}

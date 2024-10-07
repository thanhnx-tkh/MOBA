using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public enum GameState
{
    MainMenu,
    GamePlay,
    Lose,
    Win,
    Setting,
}
public class GameManager : Singleton<GameManager>
{
    public List<Enemy> enemies;
    public GameObject gamePlayUI;
    public Text textTotalEnemy;
    private static GameState gameState = GameState.MainMenu;

    // Start is called before the first frame update
    protected void Awake()
    {
        ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MianMenu>();
    }
    private void Start()
    {
        gamePlayUI.SetActive(false);
    }

    public static void ChangeState(GameState state)
    {
        gameState = state;

    }
    public void RemoveEnemytoList(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {   
            enemies.Remove(enemy);
            textTotalEnemy.text = "Total Enemy: " + enemies.Count.ToString();
        }
    }

    public static bool IsState(GameState state)
    {
        return gameState == state;
    }
    private void Update()
    {
        if (gameState == GameState.GamePlay)
        {
            gamePlayUI.SetActive(true);
        }
        else
        {
            gamePlayUI.SetActive(false);
        }
        if (enemies.Count <= 0)
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Win>();
            ChangeState(GameState.Win);
        }
    }

}

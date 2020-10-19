using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    //Game score
    private int score = 0;

    //Player Health
    private int playerHealth = 1;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        InitialUI();
    }

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            LevelManager.instance.ChangeCurrency(score);
        }
    }
    public int PlayerHealth
    {
        get { return playerHealth; }
        set
        {
            playerHealth = value;
            LevelManager.instance.ChangeHealth(playerHealth);
        }
    }

    public void InitialUI()
    {
        LevelManager.instance.ChangeCurrency(score);
        LevelManager.instance.ChangeHealth(playerHealth);
    }
}

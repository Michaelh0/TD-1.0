using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance {get; set;}
    public delegate void OnMoneyChangeEvent(int money);
    public static event OnMoneyChangeEvent onMoneyChangeEvent = delegate{};
    public delegate void OnLivesChangeEvent(int lives);
    public static event OnLivesChangeEvent onLivesChangeEvent = delegate{};
    public int startingMoney;
    public int currentMoney;
    public int startingLives;
    public int currentLives;
 
    public void ReduceMoney(int money)
    {
       currentMoney -= money;
       onMoneyChangeEvent.Invoke(currentMoney);
    }

    public void AddMoney(int money)
    {
        currentMoney += money;
        onMoneyChangeEvent.Invoke(currentMoney);
    }

    public void ReduceLives(int lives)
    {
        currentLives -= lives;
        onLivesChangeEvent.Invoke(currentLives);
    }

    public void AddLives(int lives)
    {
        currentLives += lives;
        onLivesChangeEvent.Invoke(currentLives);
    }

    public void InitializePlayerStats()
    {
        currentMoney = startingMoney;
        currentLives = startingLives;
        onMoneyChangeEvent.Invoke(currentMoney);
        onLivesChangeEvent.Invoke(currentLives);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    void Start()
    {
        InitializePlayerStats();
    }

}

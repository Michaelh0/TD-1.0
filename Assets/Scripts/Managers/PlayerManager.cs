using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager<PlayerManager>
{
    public delegate void OnMoneyChangeEvent(int money);
    public static event OnMoneyChangeEvent onMoneyChangeEvent = delegate{};
    public delegate void OnLivesChangeEvent(int lives);
    public static event OnLivesChangeEvent onLivesChangeEvent = delegate{};
    public int startingMoney;
    public int currentMoney;
    public int startingLives;
    public int currentLives;
    public float sellBackPercentage;
 
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
    public void SellTower(TowerController towerController)
    {
        towerController.gameObject.SetActive(false);
        int sellValue = (int) Mathf.Round(towerController.towerValue * sellBackPercentage);
        AddMoney(sellValue);
    }

    public int GetSellValue(TowerController towerController)
    {
        return (int) Mathf.Round(towerController.towerValue * sellBackPercentage);
    }


    protected override void Start()
    {
        
        InitializePlayerStats();
        base.Start();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatUIOverlay : UIOverlay
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    protected override void Subscribe()
    {
        PlayerManager.onLivesChangeEvent += OnUpdateLives;
        PlayerManager.onMoneyChangeEvent += OnUpdateMoney;
    }
    protected override void Unsubscribe()
    {   
        PlayerManager.onLivesChangeEvent -= OnUpdateLives;
        PlayerManager.onMoneyChangeEvent -= OnUpdateMoney;
    }

    public void OnUpdateMoney(int money)
    {
        moneyText.text = "Money: " + money.ToString();
    }

    public void OnUpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

}

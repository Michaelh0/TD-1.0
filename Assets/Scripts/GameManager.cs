using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance {get; set;}
    
    public int startingCurrency;
    public int currentCurrency;
    public int startingLives;
    public int currentLives;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        currentCurrency = startingCurrency;
        currentLives = startingLives;
        
    }

    void Start()
    {
        UIManager.Instance.UpdateLives(currentLives);
        UIManager.Instance.UpdateMoney(currentCurrency);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceLife(int damage)
    {
        
        currentLives -= damage;
        if (currentLives <= 0)
        {
            UnityEngine.Debug.Log("Game Over");
            //ui manager game over
        }
        //ui manager lives
        UIManager.Instance.UpdateLives(currentLives);
    }

    public void ReduceMoney(int cost)
    {
       currentCurrency -= cost;
       //ui manager money
       UIManager.Instance.UpdateMoney(currentCurrency);
    }

    public void AddMoney(int moneyAmount)
    {
        currentCurrency += moneyAmount;
        //ui manager
        UIManager.Instance.UpdateMoney(currentCurrency);
    }


    
}

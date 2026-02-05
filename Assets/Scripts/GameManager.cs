using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance {get; set;}
    
    public int startingCurrency;
    public int currentCurrency;
    public int startingLives;
    public int currentLives;

    public bool isFastForward;

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
        isFastForward = false;
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
            GameOver();
        }
        //ui manager lives
        UIManager.Instance.UpdateLives(currentLives);
        
    }

    public void GameOver()
    {
        UnityEngine.Debug.Log("Game Over");
        //ui manager game over
        

        SpawnManager.Instance.FreezeFrame();
        UIManager.Instance.GameOverUI();
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

    public void Restart()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void ChangeTimeScale()
    {
        isFastForward = !isFastForward;
        Time.timeScale = isFastForward ? 2.0f : 1.0f;
    }
    
}

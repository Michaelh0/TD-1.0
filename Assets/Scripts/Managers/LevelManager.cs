using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    public bool isFastForward;


    void Start()
    {
        isFastForward = false;
        PlayerManager.onLivesChangeEvent += OnLivesChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLivesChange(int currentLives)
    {
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        UnityEngine.Debug.Log("Game Over");
        //ui manager game over
        

        SpawnManager.Instance.FreezeFrame();
        UIManager.Instance.GameOverUI();
    }

    public void Restart()
    {
        //update this jank
        GameManager.LoadScene("Level 1");
        //unfreezeui
        UIManager.Instance.UnfreezeUI();
        //reset player stats
        PlayerManager.Instance.InitializePlayerStats();
        //reset wave spawner
        //deactivate the towers

    }

}

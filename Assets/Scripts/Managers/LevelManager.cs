using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    public bool HasPlayerWon => hasPlayerWon;
    public bool isFastForward;
    public bool isGameOver;
    private bool hasPlayerWon;
    protected override void Start()
    {
        isFastForward = false;
        PlayerManager.onLivesChangeEvent += OnLivesChange;
        base.Start();
    }

    public void OnLivesChange(int currentLives)
    {
        if (currentLives <= 0)
        {
            GameOver(hasWon: false);
        }
    }

    public void GameOver(bool hasWon)
    {
        hasPlayerWon = hasWon;
        
        UnityEngine.Debug.Log("Game Over");
        isGameOver = true;
        SpawnManager.Instance.SetFreezeFrame(true);
        UIManager.FreezeUI();
        UIManager.SetConfig(new GameOverUIConfig());
    }

    public void Restart()
    {
        isGameOver = false;
        //unfreezeui
        UIManager.UnfreezeUI();
        UIManager.SetConfig(new RestartUIConfig());
        //reset player stats
        PlayerManager.Instance.InitializePlayerStats();
        //reset wave spawner
        EnemyManager.DeactivateEnemies();
        //deactivate the towers
        TowerManager.DeactivateTowers();
        //deactivate the projectiles
        ProjectileManager.DeactivateProjectiles();
        EnemyManager.RestartWaveSpawner();
        SpawnManager.Instance.SetFreezeFrame(false);
    }

}

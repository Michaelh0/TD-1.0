using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : Manager<UIManager>
{
    // Start is called before the first frame update

    //different modes of ui
    //place tower mode - initiate by pressing button
    //default - no user input yet mode 
    // windows 
    // - settings
    // - tower upgrade

    // FUTURE
    // map select
    // 
    
    //worry update that happens before camera is initialized - race condition

    public TowerUIScreen towerUIScreen;
    public UpgradeUIScreen upgradeUIScreen;
    public GameOverUIScreen gameOverUIScreen;
    public PlayerStatUIOverlay playerStatUIOverlay;

    void Start()
    {
        SetInputToDefaultMode();
    }

    public static void SetInputToPlaceTowerMode()
    {
        Instance.upgradeUIScreen.InputUnsubscribe();
        Instance.towerUIScreen.InputSubscribe();
    }

    public static void SetInputToDefaultMode()
    {
        Instance.upgradeUIScreen.InputSubscribe();
        Instance.towerUIScreen.InputUnsubscribe();
    }

    public void GameOverUI()
    {
        FreezeUI();
        gameOverUIScreen.Activate();
        upgradeUIScreen.Deactivate();
    }

    public void FreezeUI()
    {
        towerUIScreen.SetInteractable(false);
        upgradeUIScreen.SetInteractable(false);
    }

    public void UnfreezeUI()
    {
        towerUIScreen.SetInteractable(true);
        upgradeUIScreen.SetInteractable(true);
    }
}

/// data structures
/// members
/// properties
/// constructors
/// inherited methods
/// class specific methods

/// static / public / protected / internal / private
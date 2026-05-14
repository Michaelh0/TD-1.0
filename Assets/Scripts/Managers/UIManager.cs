using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Linq.Expressions;


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

    // public UIScreen[] screens;
    // public UIOverlay[] overlays;

    public UICollection[] uICollections;
    
    
    protected override void Start()
    {
        base.Start();
        SetConfig(new DefaultModeUIConfig());
    }

    //Config - our visitors 
    //function Process 

    //sudo understanding visitor pattern
    public static void SetConfig(UIConfig uIConfig)
    {
        foreach(var uICollection in Instance.uICollections)
        {
            uICollection.Accept(uIConfig);
        }
    }

    public static void FreezeUI()
    {
        foreach(var uICollection in Instance.uICollections)
        {
            if (uICollection is UIScreen uIScreen)
            {
                uIScreen.SetInteractable(false);
            }
        }
    }

    public static void UnfreezeUI()
    {
        foreach(var uICollection in Instance.uICollections)
        {
            if (uICollection is UIScreen uIScreen)
            {
                uIScreen.SetInteractable(true);
            }
        }
    }
}

/// data structures
/// members
/// properties
/// constructors
/// inherited methods
/// class specific methods

/// static / public / protected / internal / private
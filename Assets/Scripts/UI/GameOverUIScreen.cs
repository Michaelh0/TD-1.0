using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIScreen : UIScreen
{
    public Button restartLevelButton;
    protected override void Subscribe()
    {
        
    }
    protected override void Unsubscribe()
    {
        
    }

    public override void SetInteractable(bool state)
    {
        
    }

    void Start()
    {
        Deactivate();
        restartLevelButton.onClick.AddListener(() => LevelManager.Instance.Restart());
    }
}


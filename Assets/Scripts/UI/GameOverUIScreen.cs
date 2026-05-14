using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUIScreen : UIScreen
{
    public TextMeshProUGUI gameOverText;
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

    public override void Activate()
    {
        base.Activate();
        if (LevelManager.Instance.HasPlayerWon)
        {
            gameOverText.text = "You Win. :)";
        }
        else
        {
            gameOverText.text = "Game Over. :(";
        }
    }
    void Start()
    {
        Deactivate();
        restartLevelButton.onClick.AddListener(() => LevelManager.Instance.Restart());
    }
}


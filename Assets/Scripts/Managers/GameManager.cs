using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string activeSceneName;
    public static GameManager Instance {get; set;}
    public bool isFastForward;

    public static void ChangeTimeScale()
    {
        Instance.isFastForward = !Instance.isFastForward;
        Time.timeScale = Instance.isFastForward ? 2.0f : 1.0f;
    }

    public static void LoadScene(string sceneName)
    {
        //maybe LoadSceneAsync
        SceneManager.LoadScene(sceneName);
        Instance.activeSceneName = sceneName;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        activeSceneName = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        isFastForward = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
}

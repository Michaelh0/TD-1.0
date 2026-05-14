using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentManager<GameManager>
{
    public string activeSceneName;
    public bool isFastForward;
    public Manager[] managers;
    public Manager[] persistentManagers;
    public int loadCounter;
    public int loadCounterPersistentManager;

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
        Instance.ResetManagers();
    }

    protected override void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            return;
        }
        activeSceneName = SceneManager.GetActiveScene().name;
        //set up persistent managers 
        InitializePersistentManagers();
        ResetManagers();
    }

    private void ResetManagers()
    {
        managers = SceneManager.GetActiveScene()
            .GetRootGameObjects()
            .First(x => x.name == "Managers")
            .GetComponentsInChildren<Manager>();

        loadCounter = 0;
        foreach(Manager manager in managers)
        {
            manager.onIsLoadedEvent += OnLoaded;
        }
        
    }

    private void InitializePersistentManagers()
    {
        persistentManagers = new[]
        {
            PersistentManagerFactory.CreatePersistentManager<DebugManager>()    
        };
        loadCounterPersistentManager = 0;
        foreach(Manager manager in persistentManagers)
        {
            manager.onIsLoadedEvent += OnLoadedPersistentManager;
        }
    }

    void OnLoadedPersistentManager(Manager manager)
    {
        manager.onIsLoadedEvent -= OnLoadedPersistentManager;
        loadCounterPersistentManager++;
        if (loadCounterPersistentManager >= persistentManagers.Length)
        {
            OnPostStartPersistentManager();
        }
    }

    private void OnPostStartPersistentManager()
    {
        foreach(Manager manager in persistentManagers)
        {
            manager.PostStart();
        }
    }
    void OnLoaded(Manager manager)
    {
        manager.onIsLoadedEvent -= OnLoaded;
        loadCounter++;
        if (loadCounter >= managers.Length)
        {
            OnPostStart();
        }
    }

    private void OnPostStart()
    {
        foreach(Manager manager in managers)
        {
            manager.PostStart();
        }
    }

    protected override void Start()
    {
        isFastForward = false;
        base.Start();
    }

}

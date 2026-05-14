using UnityEngine;

public abstract class PersistentManager<T> : Manager<T> where T : Component
{
    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else            
        {
            Destroy(gameObject);
        }
    }    
}

public static class PersistentManagerFactory
{
    public static T CreatePersistentManager<T>() where T : PersistentManager<T>
    {
        GameObject gameObject = new GameObject(typeof(T).Name, typeof(T));
        return gameObject.GetComponent<T>();
    }
}
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    
}

public abstract class Manager<T> : Manager where T : Component
{
    public static T Instance {get; set;}
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }

    }

}
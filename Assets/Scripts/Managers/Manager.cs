using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    public bool isLoaded;
    public abstract void PostStart();

    public delegate void OnIsLoadedEvent(Manager manager);
    public event OnIsLoadedEvent onIsLoadedEvent = delegate{};

    public void FireIsLoaded()
    {
        isLoaded = true;
        onIsLoadedEvent.Invoke(this);
    }

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
        else            
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Start()
    {
        FireIsLoaded();
    }

    public override void PostStart()
    {
        
    }
}
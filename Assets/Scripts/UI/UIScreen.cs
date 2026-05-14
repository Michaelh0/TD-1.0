using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIScreen : UICollection, ISubscribable, IActivatable, Interactable
{
    //state changes
    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
    //visual remove + add
    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }
    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetInteractable(bool state)
    {
        
    }
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}

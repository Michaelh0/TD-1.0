using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class UIOverlay : MonoBehaviour, ISubscribable, IActivatable
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
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
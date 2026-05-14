//ELEMENT of Visitor Pattern
using UnityEngine;
public abstract class UICollection : MonoBehaviour
{
    //default always accept 
    public virtual void Accept(UIConfig uIConfig)
    {
        uIConfig.Process(this);
    }
}

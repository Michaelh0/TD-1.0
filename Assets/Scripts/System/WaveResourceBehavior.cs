using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class WaveResourceBehavior : ScriptableObject
{
    protected float timeElapsed = 0;

    
    //could add struct if we want to add more return values
    public abstract bool ProcessSpawn(WaveResource waveResource);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Wave Resource", menuName="Scriptable Objects/Wave Resource")]
public class WaveResource : ScriptableObject
{
    public EnemyManager.EnemyID enemyType;
    public bool isCamo;
    public bool isRegen;
    public bool isFortified;
    public int enemyCount; 
    public WaveResourceBehavior waveResourceBehavior;

}


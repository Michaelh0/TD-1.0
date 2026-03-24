using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveResource : ScriptableObject
{
    // public float waitTime;
    // public float spawnRate; 
    // public int enemyCount; 
    public EnemyManager.EnemyID enemyType;

    public bool isCamo;
    public bool isRegen;
    public bool isFortified;

}

// public abstract class TowerBehavior
// {
//     public abstract void Attack(TowerController towerController);
// }

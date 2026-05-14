using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Standard Wave Resource Behavior", menuName="Scriptable Objects/Standard Wave Resource Behavior")]
public class StandardWaveResourceBehavior : WaveResourceBehavior
{   
    public float spawnRate; 
    private int currentSpawnCount = 0;
    public override bool ProcessSpawn(WaveResource waveResource)
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > spawnRate)
        {
            timeElapsed = 0;
            EnemyController enemy = EnemyManager.Spawn(waveResource.enemyType, EnemyManager.Instance.start.position); 
                
            currentSpawnCount++;
        }
        //UnityEngine.Debug.Log(currentSpawnCount);
        //UnityEngine.Debug.Log(waveResource.enemyCount);
        return currentSpawnCount >= waveResource.enemyCount;
    }

    public override void ResetWaveResourceBehavior()
    {
        currentSpawnCount = 0;
        timeElapsed = 0;
    }
}

//float spawnRate = lengthOfWave / waveResource.enemyCount;


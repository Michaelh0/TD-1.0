using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    
    public float timeElapsed;
    public Wave currentWave;
    public int currentWaveIndex;
    public int currentSpawnIndex;
    public Wave[] waves;

    [System.Serializable]
    public class Wave
    {
        public float spawnRate; //{ get; private set; }
        public int enemyCount; //{ get; private set; }
        public SpawnManager.SpawnID enemyType; //{ get; private set; }
        

        public Wave(float rate, int count, SpawnManager.SpawnID type)
        {
            spawnRate = rate;
            enemyCount = count;
            enemyType = type;
        }

    }
    

    // Start is called before the first frame update
    void Start()
    {
        currentWaveIndex = 0;
        if (waves.Length > 0)
        {
            currentWave = waves[currentWaveIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        
        if (timeElapsed > currentWave.spawnRate && currentWaveIndex < waves.Length)
        {
            timeElapsed = 0;
            if (currentSpawnIndex < currentWave.enemyCount)
            {
                EnemyManager.Spawn(currentWave.enemyType);
                currentSpawnIndex++;    

            }
            else
            {
                currentSpawnIndex = 0;
                currentWaveIndex++;
                if (waves.Length > 0 && currentWaveIndex < waves.Length)
                {
                    currentWave = waves[currentWaveIndex];
                }
            }

        }

    }
}

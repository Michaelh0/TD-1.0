using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public WaveResource currentWaveResource;
    
    //public int currentWaveResourceGroupIndex;
    public int currentWaveResourceIndex;
    public bool activeWaveResource;

    public bool isCompleted;
    
    public WaveResource[] waveResources;
    public WaveResource[] instantiatedWaveResources;
    public void SpawnNextWave()
    {
        isCompleted = false;
        currentWaveResourceIndex++;
        if (waveResources.Length > 0 && currentWaveResourceIndex < waveResources.Length)
        {
            currentWaveResource = instantiatedWaveResources[currentWaveResourceIndex];
        }
        PlayerManager.Instance.AddMoney(100 + currentWaveResourceIndex);
    }

    public void InstantiatedWaveResources()
    {
        instantiatedWaveResources = new WaveResource[waveResources.Length];

        for (int i = 0; i < waveResources.Length; i++)
        {
            instantiatedWaveResources[i] = Instantiate(waveResources[i]);
            instantiatedWaveResources[i].waveResourceBehavior = Instantiate(waveResources[i].waveResourceBehavior);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        InstantiatedWaveResources();

        currentWaveResourceIndex = 0;
        if (instantiatedWaveResources.Length > 0)
        {
            currentWaveResource = instantiatedWaveResources[currentWaveResourceIndex];
            isCompleted = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isCompleted)
        {
            isCompleted = currentWaveResource.waveResourceBehavior.ProcessSpawn(currentWaveResource);
            
        }
        
    }
}

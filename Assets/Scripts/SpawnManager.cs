using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   

    public enum SpawnID{
        enemyID,
        projectileID,
        towerID,
    }

    public static SpawnManager Instance{get; set;}
    public static GameObject Spawn(SpawnID key, Vector3 position)
    {
        if (!Instance.worldObjects.TryGetValue(key, out List<GameObject> foundObjects))
        {
            return null;
        }

        GameObject inactiveObject = foundObjects.Find(x => !x.activeSelf);
        if (inactiveObject == null)
        {
            GameObject prefab = Instance.prefabs[(int)key];
            //clone starts as active 
            GameObject clonedObject = Instantiate(prefab, position, Quaternion.identity);
            foundObjects.Add(clonedObject);
            return clonedObject;
        }

        inactiveObject.transform.position = position;
        inactiveObject.SetActive(true);
        return inactiveObject;
    }

    public GameObject[] prefabs;
    public Dictionary<SpawnID, List<GameObject>> worldObjects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    // Start is called before the first frame update
    private void Start()
    {
        worldObjects = new Dictionary<SpawnID, List<GameObject>>();
        // 0 - enemies
        worldObjects.Add(SpawnID.enemyID, new List<GameObject>());
        worldObjects.Add(SpawnID.projectileID, new List<GameObject>());
        worldObjects.Add(SpawnID.towerID, new List<GameObject>());
  
    }

    
    
}

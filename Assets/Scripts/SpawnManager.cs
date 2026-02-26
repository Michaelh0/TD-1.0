using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{   

    public enum SpawnID{
        enemy,
        projectile,
        tower,
        AOE,
    }


    public static SpawnManager Instance{get; set;}
    public static GameObject Spawn(SpawnID key, int innerKey, Vector3 position)
    {
        if (!Instance.worldObjects.TryGetValue(key, out Dictionary<int, List<GameObject>> foundDictObjects))
        {
            return null;
        }

        if (!foundDictObjects.TryGetValue(innerKey, out List<GameObject> foundObjects))
        {
            return null;
        }


        GameObject inactiveObject = foundObjects.Find(x => !x.activeSelf);
        if (inactiveObject == null)
        {
            GameObject prefab = Instance.prefabs[(int)key].listOfGameObjects[innerKey];
            //clone starts as active 
            GameObject clonedObject = Instantiate(prefab, position, Quaternion.identity);
            foundObjects.Add(clonedObject);
            return clonedObject;
        }

        inactiveObject.transform.position = position;
        inactiveObject.SetActive(true);
        return inactiveObject;
    }

    //public List<GameObject[]> prefabs;
    public GameObjectArray2d[] prefabs;
    public Dictionary<SpawnID, Dictionary<int, List<GameObject>>> worldObjects;

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
        Dictionary<int, List<GameObject>> enemyDict = new Dictionary<int, List<GameObject>>();

        foreach (EnemyManager.EnemyID enemyID in Enum.GetValues(typeof(EnemyManager.EnemyID)))
        {
            enemyDict.Add((int)enemyID, new List<GameObject>());
        }

        Dictionary<int, List<GameObject>> projectileDict = new Dictionary<int, List<GameObject>>();

        foreach (ProjectileManager.ProjectileID projectileID in Enum.GetValues(typeof(ProjectileManager.ProjectileID)))
        {
            projectileDict.Add((int)projectileID, new List<GameObject>());
        }

        Dictionary<int, List<GameObject>> towerDict = new Dictionary<int, List<GameObject>>();

        foreach (TowerManager.TowerID towerID in Enum.GetValues(typeof(TowerManager.TowerID)))
        {
            towerDict.Add((int)towerID, new List<GameObject>());
        }

        Dictionary<int, List<GameObject>> aoeDict = new Dictionary<int, List<GameObject>>();

        foreach (AOEManager.AOEID aoeID in Enum.GetValues(typeof(AOEManager.AOEID)))
        {
            aoeDict.Add((int)aoeID, new List<GameObject>());
        }

        worldObjects = new Dictionary<SpawnID, Dictionary<int, List<GameObject>>>(){
            {SpawnID.enemy, enemyDict},
            {SpawnID.projectile, projectileDict},
            {SpawnID.tower, towerDict},
            {SpawnID.AOE, aoeDict},

        };
         
        
        //worldObjects.Add();
    }

    // aka Pause
    public void FreezeFrame() 
    {

        foreach (KeyValuePair<SpawnID, Dictionary<int, List<GameObject>>> worldObjectsPair in worldObjects)
        {
            Type currentType = null;
            switch(worldObjectsPair.Key)
            { 
                case SpawnID.enemy:
                    currentType = typeof(EnemyController);
                    break;
                case SpawnID.projectile:
                    currentType = typeof(ProjectileController);
                    break;
                case SpawnID.tower:
                    currentType = typeof(TowerController);
                    break;
                case SpawnID.AOE:
                    currentType = typeof(AOEController);
                    break;
                default:
                    break;
            }  

            foreach (KeyValuePair<int, List<GameObject>> pair in worldObjectsPair.Value)
            {
                foreach(GameObject currentObject in pair.Value)
                {

                    MonoBehaviour monoBehavior = currentObject.GetComponent(currentType) as MonoBehaviour;

                    monoBehavior.enabled = false;
                }
            }
        }


    }



    
    
}

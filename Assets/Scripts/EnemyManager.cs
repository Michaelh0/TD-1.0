using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public enum EnemyID{
        red,
        blue,
        green,
        yellow,
        pink,
    }

    public static EnemyManager Instance {get; set;}
    public static EnemyController Spawn(EnemyID enemyID, Vector3 position)
    {
        //start set up in unity

        GameObject enemy = SpawnManager.Spawn(SpawnManager.SpawnID.enemyID, (int)enemyID, position);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.waypointManager = Instance.waypointManager;

        //check if enemyController exists - to initialize
        if (!Instance.enemies.Contains(enemyController))
        {
            Instance.enemies.Add(enemyController);
            enemy.name = "Enemy " + Instance.enemies.Count.ToString();
        }
        enemyController.OnSpawn(enemyID);
        return enemyController;
    }



    public void EnemyDies(EnemyController enemy)
    {
        if (enemy.enemyID > 0)
        {
            // hard coded - could be more than one damage
            EnemyID temp = enemy.enemyID - 1; 
            
            UnityEngine.Debug.Log(temp);
            EnemyController newEnemy = Spawn(temp, enemy.gameObject.transform.position);
            newEnemy.currentIndex = enemy.currentIndex;
            newEnemy.currentWaypoint = enemy.currentWaypoint;
            
        }
    }

    public Transform start;
    public WaypointManager waypointManager;
    public List<EnemyController> enemies;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

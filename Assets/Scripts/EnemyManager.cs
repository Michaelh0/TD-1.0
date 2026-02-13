using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        GameObject enemy = SpawnManager.Spawn(SpawnManager.SpawnID.enemy, (int)enemyID, position);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        

        //check if enemyController exists - to initialize
        if (!Instance.enemies.Contains(enemyController))
        {
            enemyController.waypointManager = Instance.waypointManager;
            Instance.enemies.Add(enemyController);
            
            enemy.name = "Enemy " + Instance.enemies.Count.ToString();
        }
        
        enemyController.OnSpawn(enemyID);
        return enemyController;
    }

    //possible to change sprite based on hp instead of spawn new enemy 

    //bug - two red - shot - one survived !!!!!


    public void EnemyDies(EnemyController enemy, ProjectileController ignoreProjectile)
    {
        if (enemy.enemyID > 0) // not a red balloon where enemyID = 0
        {
            // hard coded - could be more than one damage
            EnemyID temp = enemy.enemyID - 1; // effectively move to next layer of balloon
            
            UnityEngine.Debug.Log(temp);
            EnemyController newEnemy = Spawn(temp, enemy.gameObject.transform.position);
            newEnemy.currentIndex = enemy.currentIndex;
            newEnemy.currentWaypoint = enemy.currentWaypoint;
            ignoreProjectile.ignoreEnemyList.Add(newEnemy);

        }
        //dies remove from active list
        // can force spawn or auto spawn - 
        if (EnemyManager.Instance.HasNoActiveEnemies() && !waveSpawner.isActiveWave())
        {
            waveSpawner.SpawnNextWave();
        }
    }

    

    public Transform start;
    public WaypointManager waypointManager;
    public List<EnemyController> enemies;
    public WaveSpawner waveSpawner;
    public List<EnemyController> ActiveEnemies
    {
        get
        {
            //filtering for all active enemies as a list of EnemyController
            //WHERE - conditional in SQL
            return enemies.Where(enemy => enemy.gameObject.activeSelf).ToList();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    public bool HasNoActiveEnemies()
    {
        return ActiveEnemies.Count == 0;
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

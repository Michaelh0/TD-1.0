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
        
        // currentHP <= 0 // 
        int nextEnemyID = (int) enemy.enemyID - 1;
        
        int excessDamage = enemy.currentHp * -1;
        // effectively reduces enemyID to check damage
        bool hasLesserEnemy = nextEnemyID - excessDamage >= 0;
        
        if (hasLesserEnemy) 
        {
            // effectively move to next layer of balloon
            EnemyID lesserEnemy = (EnemyID) nextEnemyID - excessDamage; 
            
            //UnityEngine.Debug.Log(lesserEnemy);
            EnemyController newEnemy = Spawn(lesserEnemy, enemy.gameObject.transform.position);
            newEnemy.currentIndex = enemy.currentIndex;
            newEnemy.currentWaypoint = enemy.currentWaypoint;
            ignoreProjectile.ignoreEnemyList.Add(newEnemy);

        }
        //dies remove from active list
        // can force spawn or auto spawn - 
        
    }

    public void EnemyDies(EnemyController enemy, AOEController ignoreAOE)
    {
        
        // currentHP <= 0 // 
        int nextEnemyID = (int) enemy.enemyID - 1;
        
        int excessDamage = enemy.currentHp * -1;
        // effectively reduces enemyID to check damage
        bool hasLesserEnemy = nextEnemyID - excessDamage >= 0;
        
        if (hasLesserEnemy) 
        {
            // effectively move to next layer of balloon
            EnemyID lesserEnemy = (EnemyID) nextEnemyID - excessDamage; 
            
            //UnityEngine.Debug.Log(lesserEnemy);
            EnemyController newEnemy = Spawn(lesserEnemy, enemy.gameObject.transform.position);
            newEnemy.currentIndex = enemy.currentIndex;
            newEnemy.currentWaypoint = enemy.currentWaypoint;
            ignoreAOE.ignoreEnemyList.Add(newEnemy);

        }
        //dies remove from active list
        // can force spawn or auto spawn - 
        
    }

    public void WaveCheck()
    {
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

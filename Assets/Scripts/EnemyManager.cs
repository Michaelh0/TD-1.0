using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager Instance {get; set;}
    public static EnemyController Spawn(SpawnManager.SpawnID enemyType)
    {
        //start set up in unity
        GameObject enemy = SpawnManager.Spawn(enemyType, Instance.start.position);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.waypointManager = Instance.waypointManager;

        //check if enemyController exists - to initialize
        if (!Instance.enemies.Contains(enemyController))
        {
            Instance.enemies.Add(enemyController);
            enemy.name = "Enemy " + Instance.enemies.Count.ToString();
        }
        enemyController.OnSpawn();
        return enemyController;
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

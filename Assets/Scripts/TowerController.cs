using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public float range;
    public float attackRate;
    public float lastAttackTime;
    //public int towerCost;
    public EnemyController bestEnemy;
    
    
    // Start is called before the first frame update

    public EnemyController GetBestEnemy()
    {
        List<EnemyController> enemies = EnemyManager.Instance.enemies;
        if (enemies.Count <= 0)
        {
            return null;
        }
            

        EnemyController bestEnemy = null;

        //first best enemy is nearest to tower  CLOSE / farthest from tower:) 
        // farthest along - very hard 
        // check what the game comparison
        float nearest = float.MaxValue;
        float distance;
        for (int i = 0; i < enemies.Count; i++){
            distance = Vector3.Distance(enemies[i].transform.position,transform.position);
            if (distance <= range && enemies[i].gameObject.activeSelf)
            {
                if (distance < nearest)
                {
                    bestEnemy = enemies[i];
                    nearest = distance;
                }
                
            }
        }
        
        return bestEnemy;
    }

    public void Attack()
    {
        ProjectileController projectileController = ProjectileManager.Spawn(SpawnManager.SpawnID.projectileID, transform);
        
        
        Vector3 direction = bestEnemy.transform.position - transform.position;
        direction.Normalize();

        projectileController.direction = direction;
        // UnityEngine.Debug.Log("Attacking");
        // UnityEngine.Debug.Log(bestEnemy.gameObject.name);
    }


    void Start()
    {
        lastAttackTime = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        bestEnemy = GetBestEnemy();
        lastAttackTime += Time.deltaTime;
    
        if (bestEnemy == null)
        {
            return;
        }
    
        if (lastAttackTime >= attackRate)
        {
            Attack();
            lastAttackTime = 0;
        }

        
    }
}

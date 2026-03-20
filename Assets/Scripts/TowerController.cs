using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public float range;
    public float attackRate;
    public float lastAttackTime;
    public int towerCost;
    public ProjectileManager.ProjectileID projectileID;
    public TowerManager.TowerID towerID;
    public TowerBehavior towerBehavior;
    public EnemyController bestEnemy;
    public int damage;
    public int numOfPops;
    public int pierce;
    public TowerUpgradeGroup towerUpgradeGroup;

    public int[] towerUpgradeIndices;
    
    
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
        towerBehavior.Attack(this);
    }

    public void IncrementPops()
    {
        numOfPops++;
        UIManager.Instance.UpdateNumOfPops(this);
    }

    public void IncrementUpgradeIndex(int pathIndex)
    {
        towerUpgradeIndices[pathIndex]++;
    }


    public void UpgradeTower(TowerUpgrade towerUpgrade)
    {
        range += towerUpgrade.range;

        attackRate *= 1.0f - towerUpgrade.attackRateModifier;
        
        damage += towerUpgrade.damage;
        projectileID = towerUpgrade.projectileID;

        
        
        if(towerUpgrade.towerUpgradeComponent == null)
        {
            return;
        }
        towerUpgrade.towerUpgradeComponent.UpgradeTowerComponent(this);
        
    }

    public TowerUpgrade GetTowerUpgrade(int pathIndex)
    {
        int currentUpgradeIndex = towerUpgradeIndices[pathIndex];

        if(towerUpgradeGroup == null)
        {
            return null;
        }

        List<TowerUpgrade> selectedPath = towerUpgradeGroup.GetPath(pathIndex);

        if(currentUpgradeIndex >= selectedPath.Count)
        {
            return null;
        }
        return selectedPath[currentUpgradeIndex];
    }

    void Start()
    {
        lastAttackTime = attackRate;
        
        towerUpgradeIndices = new int[3];
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

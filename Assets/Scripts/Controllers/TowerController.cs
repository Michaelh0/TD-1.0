using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public TowerManager.TowerID towerID;
    public int towerCost;
    public float towerValue;
    public float lastAttackTime;
    public EnemyController bestEnemy;
    
    public float range;
    public float attackRate;
    public ProjectileManager.ProjectileID projectileID;
    public TowerBehavior towerBehavior;
    public int damage;
    public int numOfPops;
    public int pierce;

    private float baseRange;
    private float baseAttackRate;
    private ProjectileManager.ProjectileID baseProjectileID;
    private int baseDamage;
    private int baseNumOfPops;
    private int basePierce;
    
    public delegate void OnTowerControllerChangeEvent(TowerController towerController); 
    public OnTowerControllerChangeEvent onTowerControllerChangeEvent = delegate {};
    
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
        onTowerControllerChangeEvent.Invoke(this);
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

        towerValue += towerUpgrade.upgradeCost;
        if(towerUpgrade.towerUpgradeComponent == null)
        {
            return;
        }
        towerUpgrade.towerUpgradeComponent.UpgradeTowerComponent(this);
        onTowerControllerChangeEvent.Invoke(this);
                
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

    //when we call spawn
    public void OnSpawn()
    {
        
        range = baseRange;
        attackRate = baseAttackRate;
        projectileID = baseProjectileID;
        damage = baseDamage;
        numOfPops = baseNumOfPops;
        pierce = basePierce;
        towerValue = towerCost;
        
        lastAttackTime = attackRate;
        towerUpgradeIndices = new int[3];
        towerBehavior.Reset();
    }

    //constructor 
    private void Awake()
    {
        baseRange = range;
        baseAttackRate = attackRate;
        baseProjectileID = projectileID;
        baseDamage = damage;
        baseNumOfPops = numOfPops;
        basePierce = pierce;
        
    }

    void Start()
    {
        
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

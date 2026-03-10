using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, Ignorable, ICollidable
{
    // Start is called before the first frame update
    public Vector3 direction;
    public float speed;
    public int pierce;
    public int currentPierce;
    public float lifetime;
    public float lifetimeElapsed;
    public int damage;
    public List<EnemyController> ignoreEnemyList;
    public ProjectileCollisionBehavior projectileCollisionBehavior;
    //back call currently used for counting pops
    public TowerController towerController;
    
    public void OnSpawn()
    {
        currentPierce = 0;
        ignoreEnemyList.Clear();
    }   
    // initialize projectile so tower can talk to projectile   

    public void InitializeProjectile(TowerController towerController)
    {
        damage = towerController.damage;
        this.towerController = towerController;
    }   

    public void AddEnemyToIgnoreList(EnemyController enemy)
    {
        ignoreEnemyList.Add(enemy);
    }

    public void OnHit(EnemyController enemy)
    {
        if(ignoreEnemyList.Contains(enemy))
        {
            return;
        }
        
        bool hasCollisionBehavior = projectileCollisionBehavior != null;
        if (hasCollisionBehavior)
        {
            projectileCollisionBehavior.OnCollision(this);
        }
        else
        {
            enemy.currentHp -= damage;
            UpdatePops();
        }
        
        currentPierce++;
        //UnityEngine.Debug.Log(currentPierce);
        if (currentPierce >= pierce)
        {
            ProjectileDies();
        }

        
    }

    public void ProjectileDies()
    {
        gameObject.SetActive(false);
    }

    public void UpdatePops()
    {
        towerController.numOfPops ++;
        UIManager.Instance.UpdateNumOfPops(towerController.numOfPops);
    }

    void Start()
    {
        projectileCollisionBehavior = GetComponent<ProjectileCollisionBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeElapsed += Time.deltaTime;

        if (lifetimeElapsed >= lifetime)
        {
            ProjectileDies();
            
            lifetimeElapsed = 0;
                
        }
        
        //normalize before passing direction from TowerController
        transform.position += direction * speed * Time.deltaTime;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEController : MonoBehaviour, Ignorable, ICollidable
{
    public int pierce;
    public int currentPierce;
    public int damage;
    public float size;
    public float lifetime;
    public float lifetimeElapsed;
    public List<EnemyController> ignoreEnemyList;
    public ProjectileController projectileController;

    //possibly add queue if i want to regulate which enemy is prioritized

    public void OnSpawn()
    {
        currentPierce = 0;
        ignoreEnemyList.Clear();
    }   
    
    public void InitializeAOE(ProjectileController projectileController)
    {
        damage = projectileController.damage;
        this.projectileController = projectileController;
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
        currentPierce++;
        
        enemy.currentHp-= damage;
        
        
        projectileController.UpdatePops();
        UnityEngine.Debug.Log(currentPierce);
        if (currentPierce >= pierce)
        {
            AOEDies();
            return;
        }

    }


    public void AOEDies()
    {
        gameObject.SetActive(false);
        
    }

    void Awake()
    {
        transform.localScale = new Vector3(size, size, size);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeElapsed += Time.deltaTime;

        if (lifetimeElapsed >= lifetime)
        {
            AOEDies();
            
            lifetimeElapsed = 0;
                
        }
    }
}

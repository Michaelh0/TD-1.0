using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    public float speed;
    public int pierce;
    public int currentPierce;
    public float lifetime;
    public float lifetimeElapsed;
    public List<EnemyController> ignoreEnemyList;
    
    public void OnSpawn()
    {
        currentPierce = 0;
    }   

    
    public void OnHit(EnemyController enemy)
    {
        if(ignoreEnemyList.Contains(enemy))
        {
            return;
        }
        currentPierce++;
        ignoreEnemyList.Add(enemy);
        enemy.currentHp--;

        if (currentPierce >= pierce)
        {
            ProjectileDies();
        }
    }

    public void ProjectileDies()
    {
        gameObject.SetActive(false);
        ignoreEnemyList.Clear();
    }

    void Start()
    {
        
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

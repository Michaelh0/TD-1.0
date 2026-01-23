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
    
    public void OnSpawn()
    {
        currentPierce = 0;
    }   


    public void OnHit(EnemyController enemy)
    {
        currentPierce++;
        if (currentPierce >= pierce)
        {
            gameObject.SetActive(false);
        }
        enemy.currentHp--;
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
            gameObject.SetActive(false);
            lifetimeElapsed = 0;
        }
        
        //normalize before passing direction from TowerController
        transform.position += direction * speed * Time.deltaTime;
        
    }
}

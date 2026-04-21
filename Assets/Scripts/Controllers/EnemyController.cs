using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int currentIndex;
    public float speed;
    public float distanceThreshold;
    public int hp;
    public int currentHp;
    public int moneyValue;
    public int damageValue;
    public EnemyManager.EnemyID enemyID;
    public Transform currentWaypoint;

    public BoxCollider2D boxCollider;
    // Start is called before the first frame update

    public void OnSpawn(EnemyManager.EnemyID givenEnemyID)
    {
        currentHp = hp;
        currentIndex = 0;
        currentWaypoint = WaypointManager.Instance.GetWaypoint(currentIndex++);
        enemyID = givenEnemyID;
        //Invoke(nameof(ColliderOn), 0.5f);
        //ColliderOn();
    }   

    // public void ColliderOn()
    // {
    //     boxCollider.enabled = true;
    // }


    public void DamagePlayer()
    {
        gameObject.SetActive(false);
        PlayerManager.Instance.ReduceLives(damageValue);
        
    }

    
    void OnDisable()
    {
        EnemyManager.Instance.WaveCheck();
    }


    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypoint == null)
        {
            return;
        }
        Vector3 distanceToWaypoint = currentWaypoint.position - transform.position;
        Vector3 direction = new Vector3(distanceToWaypoint.x,distanceToWaypoint.y,distanceToWaypoint.z);
        direction.Normalize();
        // UnityEngine.Debug.Log(direction);
        transform.position += direction * speed * Time.deltaTime;
        
        if (Vector3.Distance(currentWaypoint.position,transform.position) <= distanceThreshold)
        {
            currentWaypoint = WaypointManager.Instance.GetWaypoint(currentIndex++);
            if (currentWaypoint == null)
            {
                DamagePlayer();
            }
            

        }
    }

    

    void OnTriggerEnter2D(Collider2D collider)
    {
        //collider.gameObject
        Debug.Log("GameObject1 collided with " + collider.name);
        //check its a projectile to do damage
        ICollidable collidable = collider.gameObject.GetComponentInParent<ICollidable>();
        
        
        if (collidable == null)
        {   
            UnityEngine.Debug.Log("hit by non projectile or AOE");
            return;
        }
        collidable.OnHit(this);

        if (currentHp <= 0){
            gameObject.SetActive(false);
            if (collidable is Ignorable ignorable)
            {
                EnemyManager.Instance.EnemyDies(this, ignorable);  
            }
            else
            {
                UnityEngine.Debug.Log("explodes");
            }
            
            
            PlayerManager.Instance.AddMoney(moneyValue);
        }

        //restart = true;
        //timer = 0.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public WaypointManager waypointManager;
    public int currentIndex;
    public float speed;
    public float distanceThreshold;
    public int hp;
    public int currentHp;
    public int moneyValue;
    public int damageValue;
    public Transform currentWaypoint;
    // Start is called before the first frame update

    public void OnSpawn()
    {
        currentHp = hp;
        currentIndex = 0;
        currentWaypoint = waypointManager.GetWaypoint(currentIndex++);
    }   

    public void DamagePlayer()
    {
        gameObject.SetActive(false);
        GameManager.Instance.ReduceLife(damageValue);
    }

    
    void OnEnable()
    {
        
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
            currentWaypoint = waypointManager.GetWaypoint(currentIndex++);
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
        ProjectileController projectileController = collider.gameObject.GetComponentInParent<ProjectileController>();

        if (projectileController == null)
        {
            UnityEngine.Debug.Log("hit by non projectile");
            return;
        }
        projectileController.OnHit(this);

        
        if (currentHp <= 0){
            gameObject.SetActive(false);
            GameManager.Instance.AddMoney(moneyValue);
        }

        //restart = true;
        //timer = 0.0f;
    }
}

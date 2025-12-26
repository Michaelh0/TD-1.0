using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public WaypointManager waypointManager;
    public int currentIndex;
    public float speed;
    public float distanceThreshold;
    public Transform currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {   
        currentIndex = 0;
        currentWaypoint = waypointManager.GetWaypoint(currentIndex++);
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
        UnityEngine.Debug.Log(direction);
        transform.position += direction * speed * Time.deltaTime;
        
        if (Vector3.Distance(currentWaypoint.position,transform.position) <= distanceThreshold)
        {
            currentWaypoint = waypointManager.GetWaypoint(currentIndex++);
        }
    }
}

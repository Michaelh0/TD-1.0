using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointManager : Manager<WaypointManager>
{
    public Transform[] waypoints;
    public Transform GetWaypoint(int index)
    {
        if (index >= waypoints.Length)
        {
            return null;
        }

        return waypoints[index];
    }
    protected override void Awake()
    {
        base.Awake();
        waypoints = GetComponentsInChildren<Transform>().Where(transform => transform.gameObject != gameObject).ToArray();
    }
}

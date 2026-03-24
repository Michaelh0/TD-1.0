using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
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
}

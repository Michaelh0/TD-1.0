using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionBehavior : MonoBehaviour
{
    public virtual void OnCollision(ProjectileController projectileController, EnemyController enemyController)
    {
        UnityEngine.Debug.Log("Implement Me");
    }
}

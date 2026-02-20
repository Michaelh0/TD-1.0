using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMonkeyBehavior : TowerBehavior
{
    public override void Attack(TowerController towerController)
    {
        ProjectileController projectileController = ProjectileManager.Spawn(transform, towerController.projectileID);
        
        
        Vector3 direction = towerController.bestEnemy.transform.position - transform.position;
        direction.Normalize();

        projectileController.direction = direction;
        // UnityEngine.Debug.Log("Attacking");
        // UnityEngine.Debug.Log(bestEnemy.gameObject.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTowerBehavior : TowerBehavior
{
    public override void Attack(TowerController towerController)
    {
        ProjectileController projectileController = ProjectileManager.Spawn(towerController, towerController.projectileID);
        
        Vector3 direction = towerController.bestEnemy.transform.position - towerController.gameObject.transform.position;
        direction.Normalize();

        projectileController.direction = direction;
    }
    public override void Reset()
    {
    
    }
}

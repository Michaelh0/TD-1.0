using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollisionBehavior : ProjectileCollisionBehavior
{
    public override void OnCollision(ProjectileController projectileController, EnemyController enemyController)
    {
        AOEController aoeController = AOEManager.Spawn(enemyController.transform.position, AOEManager.AOEID.bomb);
        aoeController.ignoreEnemyList.AddRange(projectileController.ignoreEnemyList);
    }
}

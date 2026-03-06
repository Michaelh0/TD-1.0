using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollisionBehavior : ProjectileCollisionBehavior
{
    public override void OnCollision(ProjectileController projectileController)
    {
        AOEController aoeController = AOEManager.Spawn(projectileController, AOEManager.AOEID.bomb);
        aoeController.ignoreEnemyList.AddRange(projectileController.ignoreEnemyList);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackShooterBehavior : TowerBehavior
{
    public int numOfProjectiles;
    

    public override void Attack(TowerController towerController)
    {

        float angle = 360f / (float) numOfProjectiles;

        for (int i = 0; i < numOfProjectiles; i++){
            
            ProjectileController projectileController = ProjectileManager.Spawn(transform, towerController.projectileID);
            
            Vector3 direction = Vector3.right;

            Quaternion angleQuaternion = Quaternion.AngleAxis(angle * i, Vector3.forward);

            // rotates Vector direction by angle
            direction = angleQuaternion * direction;
            
            direction.Normalize();
            projectileController.direction = direction;
        }
        
    }
}

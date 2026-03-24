using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName="TackShooterUpgradeComponent", menuName="Scriptable Objects/TowerUpgradeComponent/TackShooter")]
public class TackShooterUpgradeComponent : TowerUpgradeComponent<TackShooterBehavior>
{
    public override void UpgradeTowerComponent(TowerController towerController)
    {
        if (towerController.towerBehavior is TackShooterBehavior tackShooterBehavior)
        {
            tackShooterBehavior.numOfProjectiles = upgradeComponent.numOfProjectiles;
        }
        
    }
}

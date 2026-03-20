using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerUpgradeComponent : ScriptableObject
{
    public abstract void UpgradeTowerComponent(TowerController towerController);
}
public abstract class TowerUpgradeComponent<T> : TowerUpgradeComponent where T : TowerBehavior
{
    public T upgradeComponent;
}

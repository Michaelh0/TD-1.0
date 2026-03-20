using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Tower Upgrade", menuName="Scriptable Objects/Tower Upgrade")]
public class TowerUpgrade : ScriptableObject
{
    //tower upgrade added on top of current tower stats
    public float range;
    public float attackRateModifier;
    public int upgradeCost;
    public int damage;
    public int pierce;
    public ProjectileManager.ProjectileID projectileID;
    public TowerUpgradeComponent towerUpgradeComponent;
}

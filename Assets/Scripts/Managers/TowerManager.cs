using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerManager : Manager<TowerManager>
{
    // Start is called before the first frame update

    public enum TowerID{
        dartMonkey,
        tackShooter,
        bombTower,
    }

    public static TowerController Spawn(int towerType, Vector3 position)
    {
        //start set up in unity
        GameObject towerGameObject = SpawnManager.Spawn(SpawnManager.SpawnID.tower, towerType, position);
        TowerController towerController = towerGameObject.GetComponent<TowerController>();


        //check if towerController exists - to initialize
        if (!Instance.towers.Contains(towerController))
        {
            Instance.towers.Add(towerController);
            towerGameObject.name = "Tower " + Instance.towers.Count.ToString();
            towerController.towerBehavior = towerBehaviorsDict[(TowerID)towerType].Invoke();
        }
        towerController.OnSpawn();

        return towerController;
    }

    public static void DeactivateTowers()
    {
        foreach(var tower in Instance.towers)
        {
            tower.gameObject.SetActive(false);
        }
    }

    public List<TowerController> towers;
    
    //v table pattern - id map
    public static Dictionary<TowerID, Func<TowerBehavior>> towerBehaviorsDict = new Dictionary<TowerID, Func<TowerBehavior>>()
    {
        {TowerID.dartMonkey, () => new DartMonkeyBehavior()},
        {TowerID.tackShooter, () => new TackShooterBehavior()},
        {TowerID.bombTower, () => new BombTowerBehavior()},
    };

}

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography;
using System;
using UnityEngine;

[CreateAssetMenu(fileName="Tower Upgrade Group", menuName="Scriptable Objects/Tower Upgrade Group")]
public class TowerUpgradeGroup : ScriptableObject
{
    //per tower
    public List<TowerUpgrade> firstPath;
    public List<TowerUpgrade> secondPath;
    public List<TowerUpgrade> thirdPath;
    public List<TowerUpgrade> GetPath(int pathIndex)
    { 
        switch(pathIndex)
        {
            case 0: return firstPath;
            case 1: return secondPath;
            case 2: return thirdPath;
            default:
                throw new ArgumentException("Not valid path Index: " + pathIndex.ToString());
        }
    }

}

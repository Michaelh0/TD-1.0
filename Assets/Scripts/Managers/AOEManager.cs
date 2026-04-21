using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEManager : Manager<AOEManager>
{

    public enum AOEID{
        bomb,
    }

    

    //copy from spawn in enemy manager to have multiple ProjectileIDs - repeat for FUTURE tower manager
    public static AOEController Spawn(ProjectileController projectileController, AOEID aoeID)
    {
        //start set up in unity
        
        GameObject aoeGameObject = SpawnManager.Spawn(SpawnManager.SpawnID.AOE, (int) aoeID, projectileController.transform.position);
        AOEController aoeController = aoeGameObject.GetComponent<AOEController>();
        

        //check if projectileController exists - to initialize
        if (!Instance.areaOfEffects.Contains(aoeController))
        {
            Instance.areaOfEffects.Add(aoeController);
            aoeGameObject.name = "AOE " + Instance.areaOfEffects.Count.ToString();
        }
        aoeController.OnSpawn();
        aoeController.InitializeAOE(projectileController);
        return aoeController;
    }
    
    public List<AOEController> areaOfEffects;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

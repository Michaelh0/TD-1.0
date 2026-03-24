using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public enum ProjectileID{
        dart,
        tack,
        bomb,
    }

    public static ProjectileManager Instance {get; set;}

    //copy from spawn in enemy manager to have multiple ProjectileIDs - repeat for FUTURE tower manager
    public static ProjectileController Spawn(TowerController towerController, ProjectileID projectileID)
    {
        //start set up in unity
        GameObject projectileGameObject = SpawnManager.Spawn(SpawnManager.SpawnID.projectile, (int) projectileID, towerController.transform.position);
        ProjectileController projectileController = projectileGameObject.GetComponent<ProjectileController>();
                

        //check if projectileController exists - to initialize
        if (!Instance.projectiles.Contains(projectileController))
        {
            Instance.projectiles.Add(projectileController);
            projectileGameObject.name = "Projectile " + Instance.projectiles.Count.ToString();
        }
        projectileController.OnSpawn();
        projectileController.InitializeProjectile(towerController);

        return projectileController;
    }
    
    

    public List<ProjectileController> projectiles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

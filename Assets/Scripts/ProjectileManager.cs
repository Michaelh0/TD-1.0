using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance {get; set;}

    public static ProjectileController Spawn(SpawnManager.SpawnID projectileType, Transform tower)
    {
        //start set up in unity
        GameObject projectile = SpawnManager.Spawn(projectileType, tower.position);
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        

        //check if projectileController exists - to initialize
        if (!Instance.projectiles.Contains(projectileController))
        {
            Instance.projectiles.Add(projectileController);
            projectile.name = "Projectile " + Instance.projectiles.Count.ToString();
        }
        projectileController.OnSpawn();

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

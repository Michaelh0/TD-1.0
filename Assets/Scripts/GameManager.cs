using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance {get; set;}
    
    private void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
        }

    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}

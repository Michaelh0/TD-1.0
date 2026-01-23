using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    //different modes of ui
    //place tower mode - initiate by pressing button
    //default - no user input yet mode 
    // windows 
    // - settings
    // - tower upgrade

    // FUTURE
    // map select
    // 
    
    //worry update that happens before camera is initialized - race condition
    public struct TowerButtonData
    {
        public int towerId;
        public int towerCost;
        public TowerButtonData(int id, int cost)
        {
            towerId = id;
            towerCost = cost;
        }
    };

    public enum UIMode{
        defaultMode,
        towerMode,
    }

    public static UIManager Instance {get; set;}

    private Camera cam;
    
    public Button[] buttons;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;

    public float towerSize;
    public GameObject towerBlueprint;
    public int defaultCost;
    public TowerButtonData currentTowerButtonData;
    

    public UIMode currentMode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    void Start()
    {
        cam = Camera.main;

        for (int i = 0; i < buttons.Length; i++)
        {
            TowerButtonData towerdata = new TowerButtonData(i, defaultCost);
            buttons[i].onClick.AddListener(() => TowerSpawn(towerdata));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentMode)
        {
            case UIMode.towerMode:
                
                //get mouse position on screen to TRY to place tower
                Vector3 mousePos = Input.mousePosition;
        
                Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
                point.z = 0;
                //Debug.Log("Position = " + point);

                towerBlueprint.transform.position = point;

                LayerMask placeableZoneMask = LayerMask.GetMask("Placeable Zone");

                Vector2 origin = new Vector2(point.x, point.y);

                RaycastHit2D hit = Physics2D.CircleCast(origin, towerSize, Vector3.back, Mathf.Infinity, placeableZoneMask, -Mathf.Infinity, Mathf.Infinity);

                //change color

                ColorShift(hit);
                //left click
                if (Input.GetMouseButtonDown(0))
                {
                    if(hit.collider != null)
                    {
                        GameManager.Instance.ReduceMoney(currentTowerButtonData.towerCost);
                        Debug.Log("collided with = " + hit);
                        SpawnManager.Spawn(SpawnManager.SpawnID.towerID, point);
                        currentMode = UIMode.defaultMode;
                        towerBlueprint.SetActive(false);
                    }
                    //Debug.Log("hit collider = " + hit.collider);
                }
                    
                break;
            
            default:
                break;
        }

    }

    public bool CanBuyTower(TowerButtonData towerButtonData)
    {
        return GameManager.Instance.currentCurrency >= towerButtonData.towerCost;
    }

    public void UpdateMoney(int money)
    {
        moneyText.text = "Money: " + money.ToString();
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void TowerSpawn(TowerButtonData towerButtonData)
    {
        //Output this to console when the Button3 is clicked
        //swap mode to tower mode
        if (!CanBuyTower(towerButtonData)){
            return;
        }
            
        
        currentTowerButtonData = towerButtonData;
        currentMode = UIMode.towerMode;
        Debug.Log("Button clicked = " + towerButtonData.towerId);
        towerBlueprint.SetActive(true);
        
    }

    public void ColorShift(RaycastHit2D hit)
    {
        SpriteRenderer towerBlueprintSpriteRenderer = towerBlueprint.gameObject.GetComponentInChildren<SpriteRenderer>();

        float alphaValue = towerBlueprintSpriteRenderer.color.a;
        if(hit.collider == null)
        {
            //change to red
            towerBlueprintSpriteRenderer.color = Color.red;
        }
        else
        {
            //change to white - not very flexible :(
            towerBlueprintSpriteRenderer.color = Color.white;
        }
        
        
        Color clearColor = new Color(towerBlueprintSpriteRenderer.color.r, towerBlueprintSpriteRenderer.color.b, towerBlueprintSpriteRenderer.color.g, alphaValue);

        towerBlueprintSpriteRenderer.color = clearColor;
    }
}


/// members
/// properties
/// constructors
/// inherited methods
/// class specific methods

/// public / protected / internal / private
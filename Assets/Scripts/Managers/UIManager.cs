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
        public int towerID;
        public int towerCost;
        public TowerButtonData(int id, int cost)
        {
            towerID = id;
            towerCost = cost;
        }
    };

    public enum UIMode{
        defaultMode,
        placeTowerMode,
        upgradeTowerMode,
    }

    public static UIManager Instance {get; set;}

    private Camera cam;
    
    public Button[] towerButtons;
    public Button[] upgradeButtons;
    public Button leftOrientationButton;
    public Button rightOrientationButton;
    public Button restartButton;
    public Button button2x;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI towerNameText;
    public TextMeshProUGUI numOfPopsText;
    public TextMeshProUGUI[] currentPathText;
    public GameObject gameOverPanel;
    public GameObject upgradePanel;

    public float towerSize;
    public GameObject towerBlueprint;
    
    public TowerButtonData selectedTowerButtonData;
    public List<TowerButtonData> towerButtonDataList;
    public TowerController selectedTowerController;

    public UIMode currentMode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        towerButtonDataList = new List<TowerButtonData>();
    }

    void Start()
    {
        
        cam = Camera.main;
        GameObject[] towers = SpawnManager.Instance.prefabs[(int)SpawnManager.SpawnID.tower].listOfGameObjects;
        for (int i = 0; i < towerButtons.Length; i++)
        {
            
            if (towers.Length > 0 && i < towers.Length)
            {
                //has a tower
                TowerController towerController = towers[i].GetComponent<TowerController>();

                InitializeTowerButton(i,towerController);
                
            }
            else
            {
                //towerButtons[i].interactable = false;
                towerButtons[i].gameObject.SetActive(false);
            }
            
        }
        

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i;
            upgradeButtons[i].onClick.AddListener(() => UpgradeTower(index));
        }

        gameOverPanel.SetActive(false);
        upgradePanel.SetActive(false);
        restartButton.onClick.AddListener(() => GameManager.Instance.Restart());
        button2x.onClick.AddListener(() => GameManager.Instance.ChangeTimeScale());
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentMode)
        {
            case UIMode.placeTowerMode:
            {
                Vector3 worldMousePosition = GetWorldMousePosition();

                towerBlueprint.transform.position = worldMousePosition;

                LayerMask placeableZoneMask = LayerMask.GetMask("Placeable Zone");

                LayerMask towerZoneMask = LayerMask.GetMask("Tower Zone");

                Vector2 origin = GetWorldMouseOrigin(worldMousePosition);

                RaycastHit2D areaQuery = Physics2D.CircleCast(origin, towerSize, Vector3.back, Mathf.Infinity, placeableZoneMask, -Mathf.Infinity, Mathf.Infinity);

                RaycastHit2D towerQuery = Physics2D.CircleCast(origin, towerSize, Vector3.back, Mathf.Infinity, towerZoneMask, -Mathf.Infinity, Mathf.Infinity);
                
                TowerBlueprintColorShift(areaQuery.collider == null || towerQuery.collider != null);  

                
                
                //left click
                if (Input.GetMouseButtonDown(0))
                {
                    if(areaQuery.collider != null && towerQuery.collider == null)
                    {
                        GameManager.Instance.ReduceMoney(selectedTowerButtonData.towerCost);
                        TowerManager.Spawn(selectedTowerButtonData.towerID, worldMousePosition);
                        
                        currentMode = UIMode.defaultMode;
                        towerBlueprint.SetActive(false);
                    }
                    
                }
            }        
                break;
            
            case UIMode.upgradeTowerMode:

            case UIMode.defaultMode:
            {
                Vector3 worldMousePosition = GetWorldMousePosition();

                LayerMask towerZoneMask = LayerMask.GetMask("Tower Zone");

                Vector2 origin = GetWorldMouseOrigin(worldMousePosition);

                RaycastHit2D towerQuery = Physics2D.CircleCast(origin, towerSize, Vector3.back, Mathf.Infinity, towerZoneMask, -Mathf.Infinity, Mathf.Infinity);

                if (Input.GetMouseButtonDown(0))//
                {
                    if(towerQuery.collider != null)
                    {
                        UpgradePanelUI(towerQuery);
                    }
                }
            }
                break;
        }

    }

    public Vector2 GetWorldMouseOrigin(Vector3 worldMousePosition)
    {
        return new Vector2(worldMousePosition.x, worldMousePosition.y);
    }
    
    public Vector3 GetWorldMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        
        Vector3 worldMousePosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        worldMousePosition.z = 0;
        return worldMousePosition;
    }

    public void GameOverUI()
    {
        FreezeUI();
        gameOverPanel.SetActive(true);
    }

    public void FreezeUI()
    {
        for (int i = 0; i < towerButtons.Length; i++)
        {
            towerButtons[i].interactable = false;
        }
    }

    public void UpdateMoney(int money)
    {
        moneyText.text = "Money: " + money.ToString();
        UpdateTowerButton();
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void UpdateTowerName(string towerName)
    {
        towerNameText.text = towerName;
    }

    public void UpdateNumOfPops(int pops)
    {
        numOfPopsText.text = "Pops: " + pops.ToString();
    }

    public void InitializeTowerButton(int towerIndex, TowerController towerController)
    {
        TowerButtonData towerButtonData = new TowerButtonData(towerIndex, towerController.towerCost);
        towerButtons[towerIndex].onClick.AddListener(() => TowerSpawn(towerButtonData));

        TextMeshProUGUI towerName = towerButtons[towerIndex].gameObject.GetComponentInChildren<TextMeshProUGUI>();
        towerName.text = towerController.towerID.ToString() + "\n" + towerController.towerCost.ToString();

        towerButtonDataList.Add(towerButtonData);
    }

    public void UpdateTowerButton()
    {   
        
        for (int i = 0; i < towerButtonDataList.Count; i++)
        {
            towerButtons[i].interactable = CanBuyTower(towerButtonDataList[i]);    
        }
    }
    public void UpgradePanelUI(RaycastHit2D towerQuery)
    {
        selectedTowerController = towerQuery.collider.gameObject.GetComponentInParent<TowerController>();
        currentMode = UIMode.upgradeTowerMode;
        upgradePanel.SetActive(true);
        UpdateTowerName(selectedTowerController.towerID.ToString());
        UpdateNumOfPops(selectedTowerController.numOfPops);
        UpdateCurrentPaths(selectedTowerController);
        Debug.Log("collided with = " + selectedTowerController.gameObject.name);
    }
    public void UpdateCurrentPaths(TowerController towerController)
    {
        int numOfPaths = 3;
        for(int pathIndex = 0; pathIndex < numOfPaths; pathIndex++)
        {
            int towerUpgradeIndex = towerController.towerUpgradeIndices[pathIndex];

            currentPathText[pathIndex].text = towerUpgradeIndex.ToString();

            TowerUpgrade currentTowerUpgrade = towerController.GetTowerUpgrade(pathIndex);
            
            TextMeshProUGUI upgradeName = upgradeButtons[pathIndex].gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if(currentTowerUpgrade == null)
            {
                upgradeName.text = "Upgrade Complete";
                continue;
            }
            upgradeName.text = currentTowerUpgrade.upgradeCost.ToString();
        }
        
    }

    public void UpdateNumOfPops(TowerController towerController)
    {
        if (towerController != selectedTowerController)
        {
            return;
        }
        UpdateNumOfPops(towerController.numOfPops);
    }

    public bool CanBuyTower(TowerButtonData towerButtonData)
    {
        return GameManager.Instance.currentCurrency >= towerButtonData.towerCost;
    }

    public bool CanBuyUpgrade(TowerUpgrade towerUpgrade)
    {
        return GameManager.Instance.currentCurrency >= towerUpgrade.upgradeCost;
    }

    public void UpgradeTower(int pathIndex)
    {
        TowerUpgrade currentTowerUpgrade = selectedTowerController.GetTowerUpgrade(pathIndex);

        if (currentTowerUpgrade == null || !CanBuyUpgrade(currentTowerUpgrade)){
            return;
        }
        GameManager.Instance.ReduceMoney(currentTowerUpgrade.upgradeCost);
        selectedTowerController.UpgradeTower(currentTowerUpgrade);
        selectedTowerController.IncrementUpgradeIndex(pathIndex);
        UpdateCurrentPaths(selectedTowerController);
        Debug.Log("path is " + (pathIndex + 1).ToString());
        Debug.Log("selected tower: " + selectedTowerController.gameObject.name);
    }
    
    public void TowerSpawn(TowerButtonData towerButtonData)
    {
        //Output this to console when the Button3 is clicked
        //swap mode to tower mode
        if (!CanBuyTower(towerButtonData)){
            return;
        }
            
        
        selectedTowerButtonData = towerButtonData;
        currentMode = UIMode.placeTowerMode;
        Debug.Log("Button clicked = " + towerButtonData.towerID);
        towerBlueprint.SetActive(true);
        
    }

    public void TowerBlueprintColorShift(bool isRed)
    {
        SpriteRenderer towerBlueprintSpriteRenderer = towerBlueprint.gameObject.GetComponentInChildren<SpriteRenderer>();

        float alphaValue = towerBlueprintSpriteRenderer.color.a;
        if(isRed)
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUIScreen : UIScreen
{
    public class TowerButtonData
    {
        public int towerID;
        public int towerCost;
        public TowerButtonData(int id, int cost)
        {
            towerID = id;
            towerCost = cost;
        }
    };
    public Button[] towerButtons;
    public Button button2x;
    public Button cancelButton;
    public float towerSize;
    public GameObject towerBlueprint;
    public TowerButtonData selectedTowerButtonData;
    public List<TowerButtonData> towerButtonDataList = new ();
    protected override void Subscribe()
    {
        PlayerManager.onMoneyChangeEvent += OnMoneyChange;
    }
    protected override void Unsubscribe()
    {
        PlayerManager.onMoneyChangeEvent -= OnMoneyChange;
    }

    public void InputSubscribe()
    {
        InputManager.onMouseLeftClickEvent += OnQueryPlaceTower;
    }
    public void InputUnsubscribe()
    {
        InputManager.onMouseLeftClickEvent -= OnQueryPlaceTower;
    }

    

    public override void SetInteractable(bool state)
    {
        for (int i = 0; i < towerButtons.Length; i++)
        {
            towerButtons[i].interactable = state;
        }
    }

    public bool CanBuyTower(TowerButtonData towerButtonData)
    {
        return PlayerManager.Instance.currentMoney >= towerButtonData.towerCost;
    }    
    public void OnTowerButtonClick(TowerButtonData towerButtonData)
    {
        //Output this to console when the Button3 is clicked
        //swap mode to tower mode
        if (!CanBuyTower(towerButtonData)){
            return;
        }
            
        if(selectedTowerButtonData == null)
        {
            UIManager.SetConfig(new PlaceTowerModeUIConfig());
        }
        selectedTowerButtonData = towerButtonData;
        
        Debug.Log("Button clicked = " + towerButtonData.towerID);
        towerBlueprint.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }
    private void ResetToDefaultMode()
    {
        if (selectedTowerButtonData == null)
        {
            return;
        }
        selectedTowerButtonData = null;
        UIManager.SetConfig(new DefaultModeUIConfig());
        towerBlueprint.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    private void OnMoneyChange(int money)
    {
        UpdateTowerButton();
    }
    public void UpdateTowerButton()
    {   
        
        for (int i = 0; i < towerButtonDataList.Count; i++)
        {
            towerButtons[i].interactable = CanBuyTower(towerButtonDataList[i]);    
        }
    }

    private void UpdateTowerBlueprint()
    {
        LayerMask placeableZoneMask = LayerMask.GetMask("Placeable Zone");

        LayerMask towerZoneMask = LayerMask.GetMask("Tower Zone");

        Vector3 worldMousePosition = InputManager.GetWorldMousePosition();

        Vector2 origin = (Vector2) worldMousePosition;

        RaycastHit2D areaQuery = Physics2D.CircleCast(origin, towerSize, Vector3.back, Mathf.Infinity, placeableZoneMask, -Mathf.Infinity, Mathf.Infinity);

        RaycastHit2D towerQuery = Physics2D.CircleCast(origin, towerSize, Vector3.back, Mathf.Infinity, towerZoneMask, -Mathf.Infinity, Mathf.Infinity);

        towerBlueprint.transform.position = worldMousePosition;

        TowerBlueprintColorShift(areaQuery.collider == null || towerQuery.collider != null);  
    }

    private void TowerBlueprintColorShift(bool isRed)
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

    private void OnQueryPlaceTower()
    {
        LayerMask placeableZoneMask = LayerMask.GetMask("Placeable Zone");

        LayerMask towerZoneMask = LayerMask.GetMask("Tower Zone");
        
        Vector3 worldMousePosition = InputManager.GetWorldMousePosition();

        Vector2 origin = (Vector2) worldMousePosition;

        RaycastHit2D areaQuery = Physics2D.CircleCast(origin, towerSize, Vector3.back, Mathf.Infinity, placeableZoneMask, -Mathf.Infinity, Mathf.Infinity);

        RaycastHit2D towerQuery = Physics2D.CircleCast(origin, towerSize, Vector3.back, Mathf.Infinity, towerZoneMask, -Mathf.Infinity, Mathf.Infinity);

        if(areaQuery.collider != null && towerQuery.collider == null)
        {
            PlayerManager.Instance.ReduceMoney(selectedTowerButtonData.towerCost);
            TowerManager.Spawn(selectedTowerButtonData.towerID, worldMousePosition);
            ResetToDefaultMode();
        }
    } 
    
    void Start()
    {
        
        GameObject[] towers = SpawnManager.Instance.prefabs[(int)SpawnManager.SpawnID.tower].listOfGameObjects;
        for (int towerIndex = 0; towerIndex < towerButtons.Length; towerIndex++)
        {
            
            if (towers.Length > 0 && towerIndex < towers.Length)
            {
                //has a tower
                TowerController towerController = towers[towerIndex].GetComponent<TowerController>();

                TowerButtonData towerButtonData = new TowerButtonData(towerIndex, towerController.towerCost);
                towerButtons[towerIndex].onClick.AddListener(() => OnTowerButtonClick(towerButtonData));

                TextMeshProUGUI towerName = towerButtons[towerIndex].gameObject.GetComponentInChildren<TextMeshProUGUI>();
                towerName.text = towerController.towerID.ToString() + "\n" + towerController.towerCost.ToString();

                towerButtonDataList.Add(towerButtonData);
                
            }
            else
            {
                towerButtons[towerIndex].gameObject.SetActive(false);
            }
            
        }
        button2x.onClick.AddListener(() => GameManager.ChangeTimeScale());
        cancelButton.onClick.AddListener(() => ResetToDefaultMode());
        cancelButton.gameObject.SetActive(false);
        
    }
    // private void Awake()
    // {
    //     towerButtonDataList = new List<TowerButtonData>();
    // }

    void Update()
    {
        if (selectedTowerButtonData != null)
        {
            UpdateTowerBlueprint();
        }
    }
}

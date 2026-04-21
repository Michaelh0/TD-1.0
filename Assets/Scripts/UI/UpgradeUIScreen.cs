using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
public class UpgradeUIScreen : UIScreen
{
    public Button[] upgradePathButtons;
    public Button leftOrientationButton;
    public Button rightOrientationButton;
    public Button sellButton;
    public TextMeshProUGUI towerNameText;
    public TextMeshProUGUI numOfPopsText;
    public TextMeshProUGUI currentOrientation;
    public TextMeshProUGUI[] currentPathText;

    public float towerSizeQuery;
    public TowerController selectedTowerController;
    protected override void Subscribe()
    {
        if(selectedTowerController != null)
        {
            selectedTowerController.onTowerControllerChangeEvent += OnUpdateTowerControllerData;    
        }
        
    }
    protected override void Unsubscribe()
    {
        if(selectedTowerController != null)
        {
            selectedTowerController.onTowerControllerChangeEvent -= OnUpdateTowerControllerData;    
        }
    }

    public void InputSubscribe()
    {
        InputManager.onMouseLeftClickEvent += OnQueryOpenUpgradePanel;
    }
    public void InputUnsubscribe()
    {
        InputManager.onMouseLeftClickEvent -= OnQueryOpenUpgradePanel;
    }
    public override void SetInteractable(bool state)
    {
        for (int i = 0; i < upgradePathButtons.Length; i++)
        {
            upgradePathButtons[i].interactable = state;
        }
        leftOrientationButton.interactable = state;
        rightOrientationButton.interactable = state;
    }
    private void OnQueryOpenUpgradePanel()
    {
        LayerMask towerZoneMask = LayerMask.GetMask("Tower Zone");

        Vector2 origin = (Vector2) InputManager.GetWorldMousePosition();

        RaycastHit2D towerQuery = Physics2D.CircleCast(origin, towerSizeQuery, Vector3.back, Mathf.Infinity, towerZoneMask, -Mathf.Infinity, Mathf.Infinity);

        if(towerQuery.collider != null)
        {
            selectedTowerController = towerQuery.collider.gameObject.GetComponentInParent<TowerController>();
            Activate();
            UpdateTowerName(selectedTowerController.towerID.ToString());
            UpdateNumOfPops(selectedTowerController.numOfPops);
            UpdateCurrentPaths(selectedTowerController);
            UpdateSellButtonValue();
            Debug.Log("collided with = " + selectedTowerController.gameObject.name);
        }
        
    }

    private void OnUpdateTowerControllerData(TowerController towerController)
    {
        UpdateTowerName(towerController.towerID.ToString());
        UpdateNumOfPops(towerController.numOfPops);
        UpdateCurrentPaths(towerController);
        UpdateSellButtonValue();
    }

    private void UpdateTowerName(string towerName)
    {
        towerNameText.text = towerName;
    }

    private void UpdateNumOfPops(int pops)
    {
        numOfPopsText.text = "Pops: " + pops.ToString();
    }
    private void UpdateCurrentPaths(TowerController towerController)
    {
        int numOfPaths = 3;
        for(int pathIndex = 0; pathIndex < numOfPaths; pathIndex++)
        {
            int towerUpgradeIndex = towerController.towerUpgradeIndices[pathIndex];

            currentPathText[pathIndex].text = towerUpgradeIndex.ToString();

            TowerUpgrade currentTowerUpgrade = towerController.GetTowerUpgrade(pathIndex);
            
            TextMeshProUGUI upgradeName = upgradePathButtons[pathIndex].gameObject.GetComponentInChildren<TextMeshProUGUI>();
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

    public bool CanBuyUpgrade(TowerUpgrade towerUpgrade)
    {
        return PlayerManager.Instance.currentMoney >= towerUpgrade.upgradeCost;
    }

    public void OnUpgradeTowerButton(int pathIndex)
    {
        TowerUpgrade currentTowerUpgrade = selectedTowerController.GetTowerUpgrade(pathIndex);

        if (currentTowerUpgrade == null || !CanBuyUpgrade(currentTowerUpgrade)){
            return;
        }
        PlayerManager.Instance.ReduceMoney(currentTowerUpgrade.upgradeCost);
        
        selectedTowerController.UpgradeTower(currentTowerUpgrade);
        selectedTowerController.IncrementUpgradeIndex(pathIndex);
        UpdateCurrentPaths(selectedTowerController);
        UpdateSellButtonValue();
        Debug.Log("path is " + (pathIndex + 1).ToString());
        Debug.Log("selected tower: " + selectedTowerController.gameObject.name);
    }

    public void OnSellTower()
    {
        PlayerManager.Instance.SellTower(selectedTowerController);
        
        Deactivate();
    }

    public void UpdateSellButtonValue()
    {
        TextMeshProUGUI sellButtonName = sellButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        sellButtonName.text = "Sells For \n" + PlayerManager.Instance.GetSellValue(selectedTowerController).ToString();
    }

    void Start()
    {
        for (int i = 0; i < upgradePathButtons.Length; i++)
        {
            int index = i;
            upgradePathButtons[i].onClick.AddListener(() => OnUpgradeTowerButton(index));
        }
        sellButton.onClick.AddListener(() => OnSellTower());
        Deactivate();
    }
}

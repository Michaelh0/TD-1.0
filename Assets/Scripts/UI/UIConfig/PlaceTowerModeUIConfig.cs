public class PlaceTowerModeUIConfig : UIConfig
{
    public override void Process(UICollection uICollection)
    {
        System.Action<UICollection> acceptedUICollection = uICollection switch{
            UpgradeUIScreen => (x) => ProcessUpgradeScreen(uICollection as UpgradeUIScreen),
            TowerUIScreen => (x) => ProcessTowerScreen(uICollection as TowerUIScreen),
            _ => delegate(UICollection x){}
        };
        acceptedUICollection.Invoke(uICollection);
        
    }
    public void ProcessUpgradeScreen(UpgradeUIScreen upgradeUIScreen)
    {
        upgradeUIScreen.InputUnsubscribe();
    }

    public void ProcessTowerScreen(TowerUIScreen towerUIScreen)
    {
        towerUIScreen.InputSubscribe();
    }
}

public class DefaultModeUIConfig : UIConfig
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
        upgradeUIScreen.InputSubscribe();   
    }

    public void ProcessTowerScreen(TowerUIScreen towerUIScreen)
    {
        towerUIScreen.InputUnsubscribe();
    }
}

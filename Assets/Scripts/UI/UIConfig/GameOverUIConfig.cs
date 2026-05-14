public class GameOverUIConfig : UIConfig
{
    public override void Process(UICollection uICollection)
    {
        System.Action<UICollection> acceptedUICollection = uICollection switch{
            UpgradeUIScreen => (x) => ProcessUpgradeScreen(uICollection as UpgradeUIScreen),
            GameOverUIScreen => (x) => ProcessGameOverScreen(uICollection as GameOverUIScreen),
            _ => delegate(UICollection x){}
        };
        acceptedUICollection.Invoke(uICollection);
    }
    public void ProcessUpgradeScreen(UpgradeUIScreen upgradeUIScreen)
    {
        upgradeUIScreen.Deactivate();
    }

    public void ProcessGameOverScreen(GameOverUIScreen gameOverUIScreen)
    {
        gameOverUIScreen.Activate();
        gameOverUIScreen.SetInteractable(true);
    }
}

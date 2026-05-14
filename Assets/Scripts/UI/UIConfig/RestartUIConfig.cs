public class RestartUIConfig : UIConfig
{
    public override void Process(UICollection uICollection)
    {
        System.Action<UICollection> acceptedUICollection = uICollection switch{
            GameOverUIScreen => (x) => ProcessGameOverScreen(uICollection as GameOverUIScreen),
            _ => delegate(UICollection x){}
        };
        acceptedUICollection.Invoke(uICollection);
    }
    public void ProcessGameOverScreen(GameOverUIScreen gameOverUIScreen)
    {
        gameOverUIScreen.SetInteractable(false);
        gameOverUIScreen.Deactivate();
    }

}

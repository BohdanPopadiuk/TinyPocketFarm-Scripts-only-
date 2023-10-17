using UnityEngine;

public class CoinBoosterPlace : TriggerWithDelay
{
    private void Start()
    {
        GameAction.showCoinBooster += ShowCoinBoster;
    }

    private void OnDestroy()
    {
        GameAction.showCoinBooster -= ShowCoinBoster;
    }

    public void ShowCoinBoster()
    {
        int currentLevel = PlayerPrefs.GetInt(Keys.levelCompleted);
        if (currentLevel != 0 && currentLevel % 3 != 0) return;
        TriggerActivator(true);
    }

    public override void SetTrigger()
    {
        //todo show rewarded Ad
        TriggerActivator(false);
        GameAction.enableCoinBooster?.Invoke();
    }
}

public class LevelRewardButton : RewardedAdButton
{
    public override int RewardedCoinCount => Globals.instance.gameConfig.rewardForLevelComplete;
    
    public override void GetReward()
    {
        Globals.instance.AddMoney(RewardedCoinCount);
        GameAction.coinCollecting?.Invoke(transform.position);
        GameAction.gotLevelPrize?.Invoke();//???
        GAEvents.GetLevelCompletePrize();
    }
}
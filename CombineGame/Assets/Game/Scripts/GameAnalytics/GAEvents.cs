using GameAnalyticsSDK;

public static class GAEvents
{
    public static void LevelStart(int level) => GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start " + level);
    public static void LevelComplete(int level) => GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_complete " + level);
    public static void LevelRestart(int level) => GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "level_restart " + level);
    
    public static void ResetProgress() => GameAnalytics.NewDesignEvent("ResetProgress");
    
    public static void BuySkin(string name) => GameAnalytics.NewDesignEvent("buy_skin " + name);
    public static void BuyPrint(string name) => GameAnalytics.NewDesignEvent("buy_print " + name);
    
    public static void UpgradeParametr(string parametrName, int level) => GameAnalytics.NewDesignEvent(parametrName + " " + level);
    
    public static void ShowInterstitialAd() => GameAnalytics.NewDesignEvent("show_interstitial_ad");
    public static void GetLevelCompletePrize() => GameAnalytics.NewDesignEvent("get_level_complete_prize");
    
    public static void BuyFreeIAP() => GameAnalytics.NewDesignEvent("get_free_coin_pack");
    public static void BuySmallCoinPack() => GameAnalytics.NewDesignEvent("buy_small_coin_pack");
    public static void BuyBigCoinPack() => GameAnalytics.NewDesignEvent("buy_big_coin_pack");
    public static void BuyMegaCoinPack() => GameAnalytics.NewDesignEvent("buy_mega_coin_pack");
    
    public static void OpenGame() => GameAnalytics.NewDesignEvent("open_game");
}

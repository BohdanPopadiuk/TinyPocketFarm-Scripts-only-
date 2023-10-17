using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{
    public static Globals instance;
    public GameConfig gameConfig;
    public SkinData skinData;
    
    public int usedCapacity;
    public int previousMoneyCount;
    public int levelPlayedPerSesion;

    public bool devMode;
    public bool removedAd;

    public float PlayerSpeed;
    public float PlayerCapacity;
    
    public bool fullCapacity => usedCapacity >= PlayerCapacity;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Destroying duplicate Globals object - only one is allowed per scene!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        
        SetAdRemoved();
        devMode = false;
        Application.targetFrameRate = 60;
        CalculatePlayerLevel();
    }

    public void CalculatePlayerLevel()
    {
        PlayerSpeed = gameConfig.playerSpeed + gameConfig.maxSpeedUpgrade /
            gameConfig.speedUpgradePrice.Count * PlayerPrefs.GetInt(Keys.speedLevel);
        
        PlayerCapacity = gameConfig.playerCapacity + gameConfig.maxCapacityUpgrade /
            gameConfig.capacityUpgradePrice.Count * PlayerPrefs.GetInt(Keys.capacityLevel);
    }

    public bool UseMoney(int price)
    {
        int moneyCount = PlayerPrefs.GetInt(Keys.moneyCount) - price;
        if (moneyCount >= 0)
        {
            PlayerPrefs.SetInt(Keys.moneyCount, moneyCount);
            GameAction.spendMoney?.Invoke();
            return true;
        }
        GameAction.openIAP?.Invoke();
        return false;
    }

    public void AddMoney(int moneyCount)
    {
        previousMoneyCount = PlayerPrefs.GetInt(Keys.moneyCount);
        PlayerPrefs.SetInt(Keys.moneyCount, previousMoneyCount + moneyCount);
    }


    void SetAdRemoved()
    {
        removedAd = PlayerPrefs.GetInt(Keys.removedAd) == 1;
    }//ToDo Check IAP

    public void RemoveAds()
    {
        removedAd = true;
        PlayerPrefs.SetInt(Keys.removedAd, 1);
    }
    
    public void ClearALL()
    {
        GAEvents.ResetProgress();
        PlayerPrefs.DeleteAll();
        CalculatePlayerLevel();
        usedCapacity = 0;
        SceneManager.LoadScene(1);
    }
}

public static class Keys // id, links, keys for PlayerPrefs
{
    public static readonly string bannerId = "ca-app-pub-3940256099942544/6300978111";//test key
    public static readonly string interstitialId = "ca-app-pub-3940256099942544/1033173712";//test key
    public static readonly string rewardedId = "ca-app-pub-3940256099942544/5224354917";//test key

    public static readonly  string currentScene = "currentScene";
    public static readonly string levelCompleted = "levelCompleted";
    public static readonly string moneyCount = "moneyCount";
    public static readonly string currentSkin = "currentSkin";
    public static readonly string currentPrint = "currentPrint";
    public static readonly string removedAd = "removedAd";
    
    public static readonly string speedLevel = "speedLevel";
    public static readonly string capacityLevel = "capacityLevel";

    public static readonly string privacyPolicyURL = "https://sites.google.com/view/tiny-pocket-farm/privacy-policy";
    public static readonly string rateUsURL = "https://play.google.com/store/apps/details?id=com.IncognitoHatGames.TinyPocketFarm";
    public static readonly string soundEnabled = "soundEnabled";
}

public static class GameAction
{
    public static Action upgradePlayer;
    public static Action updateCapacityIndicator;
    public static Action updateProgressIndicator;
    public static Action updateCoinCounter;
    public static Action gotLevelPrize;
    public static Action<Vector3> coinCollecting;
    public static Action<bool> changeSoundState;
    public static Action<bool> grassFall;
    public static Action spendMoney;
    public static Action<PrintButton> setPrintPrice;
    public static Action<Material> sellectPrint;
    public static Action openIAP;
    public static Action levelComplete;
    public static Action showCoinBooster;
    public static Action enableCoinBooster;
    public static Action showNextLevelButton;
    public static Action showTransitionScreen;
    public static Action openNextLevel;
    public static Action grainSale;
    public static Action<bool> stayAtUpgradingPlace;
    public static Action noIntenet;
    public static Action selectSkin;
    public static Action newSkinBuyed;
    public static Action restartLevel;
}

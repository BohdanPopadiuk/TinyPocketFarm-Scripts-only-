using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private GameConfig gameConfig;
    [SerializeField] private GameObject capacityCoin;
    [SerializeField] private GameObject speedCoin;
    [SerializeField] private Button upgradeSpeedButton;
    [SerializeField] private Button upgradeCapacityButton;
    [SerializeField] private Image speedLevelBar;
    [SerializeField] private Image capacityLevelBar;
    [SerializeField] private TextMeshProUGUI speedPrice;
    [SerializeField] private TextMeshProUGUI capacityPrice;

    [SerializeField] private Transform speedBarCells;
    [SerializeField] private Transform capacityBarCells;

    private bool maxCapacityLevel => PlayerPrefs.GetInt(Keys.capacityLevel) >= gameConfig.capacityUpgradePrice.Count;
    private bool maxSpeedLevel => PlayerPrefs.GetInt(Keys.speedLevel) >= gameConfig.speedUpgradePrice.Count;

    private void Start()
    {
        upgradeSpeedButton.onClick.AddListener(()=> UpgradeSpeed());
        upgradeCapacityButton.onClick.AddListener(()=> UpgradeCapacity());
        
        gameConfig = Globals.instance.gameConfig;
        SetUpgradeInfo();
        CreateBarCells(speedBarCells, gameConfig.speedUpgradePrice.Count);
        CreateBarCells(capacityBarCells, gameConfig.capacityUpgradePrice.Count);
    }

    void UpgradeSpeed()
    {
        Upgrade(Keys.speedLevel, gameConfig.speedUpgradePrice, maxSpeedLevel);
    }

    void UpgradeCapacity()
    {
        Upgrade(Keys.capacityLevel, gameConfig.capacityUpgradePrice, maxCapacityLevel);
        
    }

    void Upgrade(string parameter, List<int> price, bool maxLevel)
    {
        if(maxLevel) return;
        bool canBuy = Globals.instance.UseMoney(price[PlayerPrefs.GetInt(parameter)]);
        if(!canBuy) return;

        int newParametrLevel = PlayerPrefs.GetInt(parameter) + 1;
        PlayerPrefs.SetInt(parameter, newParametrLevel);
        GAEvents.UpgradeParametr(parameter, newParametrLevel);
        
        Globals.instance.CalculatePlayerLevel();
        GameAction.upgradePlayer?.Invoke();
        SetUpgradeInfo();
    }

    void SetUpgradeInfo()
    {
        upgradeSpeedButton.interactable = !maxSpeedLevel;
        upgradeCapacityButton.interactable = !maxCapacityLevel;

        capacityCoin.SetActive(!maxCapacityLevel);
        speedCoin.SetActive(!maxSpeedLevel);
        
        speedPrice.text = maxSpeedLevel ? "MAX LEVEL" :
            gameConfig.speedUpgradePrice[PlayerPrefs.GetInt(Keys.speedLevel)].ToString();
        
        capacityPrice.text = maxCapacityLevel ? "MAX LEVEL" :
            gameConfig.capacityUpgradePrice[PlayerPrefs.GetInt(Keys.capacityLevel)].ToString();

        speedLevelBar.fillAmount = (float)PlayerPrefs.GetInt(Keys.speedLevel) / gameConfig.speedUpgradePrice.Count;
        capacityLevelBar.fillAmount = (float)PlayerPrefs.GetInt(Keys.capacityLevel) / gameConfig.capacityUpgradePrice.Count;
    }

    void CreateBarCells(Transform barCells, int cellsCount)
    {
        GameObject cell = barCells.GetChild(1).gameObject;
        for (int i = 0; i < cellsCount - 2; i++)
        {
            Instantiate(cell, barCells);
        }
    }
}

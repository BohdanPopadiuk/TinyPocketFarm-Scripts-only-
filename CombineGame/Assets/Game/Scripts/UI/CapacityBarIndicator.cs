using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CapacityBarIndicator : MonoBehaviour
{
    
    [SerializeField] private Image capcityBackgound;
    [SerializeField] private Image capacityIndicator;
    [SerializeField] private List<CanvasGroup> coinIcons;
    [SerializeField] private AddCoinAnimation[] singleCoins;
    [SerializeField] private TextMeshProUGUI moneyCounter;
    [SerializeField] private Transform endCoinPoint;
    [SerializeField] private float maxBarSize;
    [SerializeField] private float baseBarSize;
    private int currentCoinPoint;
    
    void Start()
    {
        SetBarSize();
        GameAction.grassFall += FadeOffCoin;
        GameAction.grainSale += GrainSale;
        GameAction.updateCapacityIndicator += UpdateCapacityIndicator;
        GameAction.updateProgressIndicator += UpdateCapacityIndicator;
        GameAction.upgradePlayer += SetBarSize;
    }

    private void OnDestroy()
    {
        GameAction.grassFall -= FadeOffCoin;
        GameAction.grainSale -= GrainSale;
        GameAction.updateCapacityIndicator -= UpdateCapacityIndicator;
        GameAction.updateProgressIndicator -= UpdateCapacityIndicator;
        GameAction.upgradePlayer -= SetBarSize;
    }

    void UpdateCapacityIndicator()
    {
        capacityIndicator.fillAmount = (float)Globals.instance.usedCapacity / Globals.instance.PlayerCapacity;
    }

    void SetBarSize()
    {
        float newBarSize = baseBarSize + (float)PlayerPrefs.GetInt(Keys.capacityLevel) /
            Globals.instance.gameConfig.capacityUpgradePrice.Count * (maxBarSize - baseBarSize);
        Debug.Log("Bar size " + newBarSize);
        capcityBackgound.rectTransform.sizeDelta = new Vector2(newBarSize, capcityBackgound.rectTransform.sizeDelta.y);
        capacityIndicator.rectTransform.sizeDelta = new Vector2(newBarSize, capacityIndicator.rectTransform.sizeDelta.y);
        UpdateCapacityIndicator();
        SetCoinPoint();
    }

    void GrainSale()
    {
        SetCoinPoint(true);
    }

    void SetCoinPoint(bool playCoinAnimation = false)
    {
        currentCoinPoint = Globals.instance.usedCapacity / 40;
        if (currentCoinPoint != 0)
        {
            coinIcons[currentCoinPoint - 1].DOFade(0, .5f);
            
            if (playCoinAnimation)
            {
                StartCoroutine(singleCoins[currentCoinPoint].AddCoin(
                    coinIcons[currentCoinPoint - 1].transform.position, endCoinPoint, moneyCounter));
            }
        }
        if (Globals.instance.PlayerCapacity >= (currentCoinPoint + 1) * 40)
        {
            coinIcons[currentCoinPoint].DOFade(1, .5f);
        }
    }

    void FadeOffCoin(bool grassFall)
    {
        if (Globals.instance.PlayerCapacity < (currentCoinPoint + 1) * 40) currentCoinPoint--;
        Debug.Log(currentCoinPoint);
        if (grassFall) coinIcons[currentCoinPoint].DOFade(0, .5f);
        else SetCoinPoint();
    }
}

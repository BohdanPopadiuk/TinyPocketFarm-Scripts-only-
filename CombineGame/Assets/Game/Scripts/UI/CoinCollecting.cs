using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollecting : MonoBehaviour
{
    [SerializeField] private UIPageController IAPPage;
    [SerializeField] private GameObject spendMoneyParticle;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform pileOfCoins;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private CanvasGroup coinBoosterIcon;
    [SerializeField] private Transform coinBoosterPivot;
    [SerializeField] private float coinBoosterShowSpeed = 0.7f;
    private Vector2[] initialPos;
    private Vector3[] initialRotation;
    private int coinCount;
    void Start()
    {
        coinCount = pileOfCoins.childCount;
        initialPos = new Vector2[coinCount];
        initialRotation = new Vector3[coinCount];
        
        for (int i = 0; i < coinCount; i++)
        {
            initialPos[i] = pileOfCoins.GetChild(i).localPosition;
            initialRotation[i] = pileOfCoins.GetChild(i).localEulerAngles;
        }
        
        UpdateCoinCounter();
        GameAction.enableCoinBooster += ShowCoinBoosterIcon;
        GameAction.updateCoinCounter += UpdateCoinCounter;
        GameAction.coinCollecting += CollectCoins;
        GameAction.spendMoney += SpendMoney;
        GameAction.openIAP += OpenIAP;
    }

    private void OnDestroy()
    {
        GameAction.enableCoinBooster -= ShowCoinBoosterIcon;
        GameAction.updateCoinCounter -= UpdateCoinCounter;
        GameAction.coinCollecting -= CollectCoins;
        GameAction.spendMoney -= SpendMoney;
        GameAction.openIAP -= OpenIAP;
    }

    private void SpendMoney()
    {
        UpdateCoinCounter();
        StartCoroutine(PlayUiParticles());
        counter.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    IEnumerator PlayUiParticles()
    {
        GameObject newParticle = Instantiate(spendMoneyParticle, endPoint);
        newParticle.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(1);
        Destroy(newParticle);
    }

    private void CollectCoins(Vector3 coinSpawnPoint)
    {
        pileOfCoins.position = new Vector3(coinSpawnPoint.x, coinSpawnPoint.y, pileOfCoins.position.z);
        
        int previousMoneyCount = Globals.instance.previousMoneyCount;
        int currentManeyCount = PlayerPrefs.GetInt(Keys.moneyCount);
        int counterUpdateStep = (currentManeyCount - previousMoneyCount) / coinCount;
        
        var delay = 0f;

        for (int i = 0; i < coinCount; i++)
        {
            Transform coin = pileOfCoins.GetChild(i).transform;
            
            coin.DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
            coin.DOMove(new Vector2(endPoint.position.x, endPoint.position.y), 0.6f)
                .SetDelay(delay + 0.5f).SetEase(Ease.InBack);
            coin.DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);
            coin.DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            int newMoneyCount = previousMoneyCount + counterUpdateStep * i;
            if (i + 1 >= coinCount) newMoneyCount = currentManeyCount;
            StartCoroutine(GraduallyUpdateCoinCount(newMoneyCount, delay));

            delay += 0.1f;
            
            //todo update counter text
            
            coin.localPosition = initialPos[i];
            coin.localEulerAngles = initialRotation[i];
        }
    }

    IEnumerator GraduallyUpdateCoinCount(int newMoneyCount, float delay)
    {
        yield return new WaitForSecondsRealtime(delay + 1.1f);
        counter.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        counter.text = newMoneyCount.ToString();
    }

    void OpenIAP()
    {
        IAPPage.OpenPage();
    }

    void ShowCoinBoosterIcon()
    {
        StartCoroutine(ShowCoinBoosterIconAnim());
    }

    IEnumerator ShowCoinBoosterIconAnim()
    {
        coinBoosterIcon.DOFade(1, coinBoosterShowSpeed).SetEase(Ease.OutBack);
        coinBoosterIcon.transform.DOScale(3, coinBoosterShowSpeed).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(coinBoosterShowSpeed);
        coinBoosterIcon.transform.parent = coinBoosterPivot;
        coinBoosterIcon.transform.DOScale(1, coinBoosterShowSpeed).SetEase(Ease.InOutBack);
        coinBoosterIcon.transform.DOLocalMove(Vector3.zero, coinBoosterShowSpeed).SetEase(Ease.InOutBack);
    }

    void UpdateCoinCounter()
    {
        counter.text = PlayerPrefs.GetInt(Keys.moneyCount).ToString();
    }
}
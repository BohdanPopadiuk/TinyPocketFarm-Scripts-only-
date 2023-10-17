using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPCore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI smallCoinPackCoinsText;
    [SerializeField] private TextMeshProUGUI bigCoinPackCoinsText;
    [SerializeField] private TextMeshProUGUI megaCoinPackCoinsText;
    
    [SerializeField] private TextMeshProUGUI smallCoinPackPriceText;
    [SerializeField] private TextMeshProUGUI bigCoinPackPriceText;
    [SerializeField] private TextMeshProUGUI megaCoinPackPriceText;

    void Start()
    {
        smallCoinPackCoinsText.text = Globals.instance.gameConfig.rewardFor_1stIAP.ToString();
        bigCoinPackCoinsText.text = Globals.instance.gameConfig.rewardFor_2ndIAP.ToString();
        megaCoinPackCoinsText.text = Globals.instance.gameConfig.rewardFor_3rdIAP.ToString();
    }

    public void OnPurchaseCompleted(Product product)
    {
        Globals.instance.RemoveAds();
        switch (product.definition.id)
        {
            case "smallcoinpack":

                Globals.instance.AddMoney(Globals.instance.gameConfig.rewardFor_1stIAP);
                GameAction.coinCollecting?.Invoke(smallCoinPackCoinsText.transform.parent.position);
                Debug.Log("BuySmallCoinPack");
                GAEvents.BuySmallCoinPack();

                break;
            case "bigcoinpack":

                Globals.instance.AddMoney(Globals.instance.gameConfig.rewardFor_2ndIAP);
                GameAction.coinCollecting?.Invoke(bigCoinPackCoinsText.transform.parent.position);
                Debug.Log("BuyBigCoinPack");
                GAEvents.BuyBigCoinPack();

                break;
            case "megacoinpack_2":

                Globals.instance.AddMoney(Globals.instance.gameConfig.rewardFor_3rdIAP);
                GameAction.coinCollecting?.Invoke(megaCoinPackCoinsText.transform.parent.position);
                Debug.Log("BuyMegaCoinPack");
                GAEvents.BuyMegaCoinPack();

                break;
        }
    }

    public void UpdateCoinPrice(Product product)
    {
        switch (product.definition.id)
        {
            case "smallcoinpack":
                smallCoinPackPriceText.text = product.metadata.localizedPrice.ToString("F2");
                break;
            case "bigcoinpack":
                bigCoinPackPriceText.text = product.metadata.localizedPrice.ToString("F2");
                break;
            case "megacoinpack_2":
                megaCoinPackPriceText.text = product.metadata.localizedPrice.ToString("F2");
                break;
        }
    }
}

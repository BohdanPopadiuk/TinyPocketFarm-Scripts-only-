using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdButton : MonoBehaviour
{
    public TextMeshProUGUI rewardedCoinsCountText;
    public virtual int RewardedCoinCount => Globals.instance.gameConfig.rewardForAD;
    public void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(()=> RewardedAdController.instance.ShowRewardedAd(this));
        rewardedCoinsCountText.text = RewardedCoinCount.ToString();
    }

    public virtual void GetReward()
    {
        Globals.instance.AddMoney(RewardedCoinCount);
        GameAction.coinCollecting?.Invoke(transform.position);
        GAEvents.BuyFreeIAP();
        Debug.Log("GetReward");
    }
}

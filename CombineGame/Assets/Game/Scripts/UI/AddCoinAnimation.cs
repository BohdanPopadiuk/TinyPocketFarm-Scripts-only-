using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AddCoinAnimation : MonoBehaviour//todo тут дублюється код з класи CoinCollecting це все для тесту потім перероблю
{
    private bool coinBoosterIsActive;
    private void Start() => GameAction.enableCoinBooster += ActivateCoinBooster;
    private void OnDestroy() => GameAction.enableCoinBooster -= ActivateCoinBooster;
    private void ActivateCoinBooster() => coinBoosterIsActive = true;

    public IEnumerator AddCoin(Vector3 coinSpawnPoint, Transform endPoint, TextMeshProUGUI counter)
    {
        transform.position = new Vector3(coinSpawnPoint.x, coinSpawnPoint.y, transform.position.z);
        
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        transform.DOMove(new Vector2(endPoint.position.x, endPoint.position.y), 0.6f)
            .SetDelay(0.5f).SetEase(Ease.InBack);
        transform.DORotate(Vector3.zero, 0.5f).SetDelay(0.5f).SetEase(Ease.Flash);
        transform.DOScale(0f, 0.3f).SetDelay(1.5f).SetEase(Ease.OutBack);

        counter.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine).SetDelay(1.2f);

        yield return new WaitForSeconds(1.1f);
        Globals.instance.AddMoney(Globals.instance.gameConfig.rewardForWheat * (coinBoosterIsActive ? 2 : 1));
        GameAction.updateCoinCounter?.Invoke();
    }
}
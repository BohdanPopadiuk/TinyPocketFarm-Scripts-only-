using DG.Tweening;
using UnityEngine;
public class NoInternetPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.transform.localScale = Vector3.zero;
        canvasGroup.alpha = 0;
        GameAction.noIntenet += ShowNoInternetPanel;
    }
    private void OnDestroy()
    {
        GameAction.noIntenet -= ShowNoInternetPanel;
    }

    void Update() { if (Input.GetKeyDown(KeyCode.Space)) ShowNoInternetPanel(); }
    
    void ShowNoInternetPanel()
    {
        float animDuration = 1;
        canvasGroup.transform.DOScale(1, animDuration).SetEase(Ease.InOutBack);
        canvasGroup.DOFade(1, animDuration).SetEase(Ease.InOutBack);
        canvasGroup.transform.DOScale(0, animDuration).SetEase(Ease.InOutBack).SetDelay(animDuration + .5f);
        canvasGroup.DOFade(0, animDuration).SetEase(Ease.InOutBack).SetDelay(animDuration + .5f);
    }
}

using System.Collections;
using DG.Tweening;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    private GameConfig gameConfig;
    
    [SerializeField] private CanvasGroup winPanel;
    [SerializeField] private CanvasGroup noThanksButton;
    [SerializeField] private CanvasGroup winPrizeButton;
    [SerializeField] private CanvasGroup nextLevelButton;
    [SerializeField] private float respawnButtonSpeed = 0.7f;
    
    void Start()
    {
        gameConfig = Globals.instance.gameConfig;

        GameAction.gotLevelPrize += GotLevelPrize;
        GameAction.levelComplete += LevelComplete;
    }

    private void OnDestroy()
    {
        GameAction.gotLevelPrize -= GotLevelPrize;
        GameAction.levelComplete -= LevelComplete;
    }

    [ContextMenu("TestLevelComplete")]
    private void TestLevelComplete()
    {
        GameAction.levelComplete();
        LevelComplete();
    }

    public void LevelComplete()
    {
        StartCoroutine(ShowWinPanel());
    }

    public void OpenNextLevel()
    {
        GameAction.openNextLevel?.Invoke();
    }
    
    IEnumerator ShowWinPanel()
    {
        winPanel.transform.localScale = new Vector3(.5f, .5f, .5f);
        
        winPanel.transform.SetAsLastSibling();
        winPanel.transform.DOScale(1, gameConfig.winPanelShowSpeed / 3).SetEase(Ease.OutBack);
        winPanel.DOFade(1, gameConfig.winPanelShowSpeed);
        winPanel.blocksRaycasts = true;
        yield return new WaitForSeconds(gameConfig.winButtonShowDuration);
        noThanksButton.DOFade(1, gameConfig.winPanelShowSpeed);
        noThanksButton.blocksRaycasts = true;
    }
    void GotLevelPrize() => StartCoroutine(HidePrizeButtons());
    private IEnumerator HidePrizeButtons()
    {
        noThanksButton.blocksRaycasts = false;
        winPrizeButton.blocksRaycasts = false;
        noThanksButton.DOFade(0, respawnButtonSpeed);
        winPrizeButton.DOFade(0, respawnButtonSpeed);
        noThanksButton.transform.DOScale(0, respawnButtonSpeed);
        winPrizeButton.transform.DOScale(0, respawnButtonSpeed).SetEase(Ease.InOutCirc);
        yield return new WaitForSeconds(respawnButtonSpeed);
        nextLevelButton.blocksRaycasts = true;
        nextLevelButton.DOFade(1, respawnButtonSpeed);
        nextLevelButton.transform.DOScale(1, respawnButtonSpeed);
    }
}

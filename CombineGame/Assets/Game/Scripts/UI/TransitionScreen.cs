using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private CanvasGroup background;
    [SerializeField] private float hidePointX = 650;
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt(Keys.levelCompleted);
        if (SceneManager.GetActiveScene().buildIndex != 0) HideTransition(currentLevel);
        levelNumberText.text = "LEVEL " + currentLevel;
        GameAction.showTransitionScreen += ShowTransition;
    }

    private void OnDestroy()
    {
        GameAction.showTransitionScreen -= ShowTransition;
    }

    [ContextMenu("ShowTransition")]
    public void ShowTransition()
    {
        background.alpha = 0;
        levelNumberText.transform.localPosition = new Vector3(hidePointX, 0, 0);
        levelNumberText.text = "LEVEL " + (PlayerPrefs.GetInt(Keys.levelCompleted));
        levelNumberText.transform.DOLocalMoveX(0, .37f);
        background.DOFade(1, .37f);
    }

    [ContextMenu("HideTransition")]
    void HideTransition(int level)
    {
        background.alpha = 1;
        levelNumberText.transform.localPosition = Vector3.zero;
        background.DOFade(0, .38f).SetDelay(.12f);
        levelNumberText.transform.DOLocalMoveX(-hidePointX, .38f).SetDelay(.12f);
        GAEvents.LevelStart(level);
    }
}

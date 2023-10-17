using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Button privacyPolicyButton;
    [SerializeField] private Button rateUsButton;
    [SerializeField] private Button removeAd;
    [SerializeField] private Button resetProgress;
    [SerializeField] private Button restartLevel;

    void Start()
    {
        privacyPolicyButton.onClick.AddListener(() => OpenURL(Keys.privacyPolicyURL));
        rateUsButton.onClick.AddListener(() => OpenURL(Keys.rateUsURL));
        removeAd.onClick.AddListener(() => OpenIAPPanel());
        resetProgress.onClick.AddListener(() => Globals.instance.ClearALL());
        restartLevel.onClick.AddListener(() => RestartLevel());
    }
    
    void OpenIAPPanel()
    {
        GameAction.openIAP?.Invoke();
    }
    
    void OpenURL(string link)
    {
        Application.OpenURL(link);
    }

    void RestartLevel()
    {
        GAEvents.LevelRestart(PlayerPrefs.GetInt(Keys.levelCompleted));
        GameAction.restartLevel?.Invoke();
    }
}

using UnityEngine.UI;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    private bool soundEnabled;
    private CanvasGroup canvasGroup;
    [SerializeField] private GameObject enableIcon;
    [SerializeField] private GameObject disableIcon;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => SoundSwitch());

        soundEnabled = PlayerPrefs.GetInt(Keys.soundEnabled) == 0;
        SetButtonState();
    }

    void SoundSwitch()
    {
        soundEnabled = !soundEnabled;
        PlayerPrefs.SetInt(Keys.soundEnabled, soundEnabled ? 0 : 1);
        GameAction.changeSoundState?.Invoke(soundEnabled);
        SetButtonState();
    }

    void SetButtonState()
    {
        canvasGroup.alpha = soundEnabled ? 1 : 0.5f;
        disableIcon.SetActive(!soundEnabled);
        enableIcon.SetActive(soundEnabled);
    }
}

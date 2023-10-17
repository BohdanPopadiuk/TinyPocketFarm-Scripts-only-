using System.Collections;
using DG.Tweening;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private CanvasGroup splashScreen;
    IEnumerator Start()
    {
        int currentScene = PlayerPrefs.GetInt(Keys.currentScene);
        if (currentScene == 0) currentScene = 1;//tutorial
        else if (currentScene >= SceneManager.sceneCountInBuildSettings) currentScene = 2;
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentScene);
        asyncLoad.allowSceneActivation = false;
        
        GameAnalytics.Initialize();
        GAEvents.OpenGame();
        
        splashScreen.transform.DOScale(1.2f, 2.5f);
        splashScreen.DOFade(0, 1.5f).SetDelay(1);
        
        yield return new WaitForSeconds(2.5f);
        GameAction.showTransitionScreen?.Invoke();
        yield return new WaitForSeconds(.38f);
        asyncLoad.allowSceneActivation = true;
    }
}

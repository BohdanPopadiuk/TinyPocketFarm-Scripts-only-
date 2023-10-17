using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Awake()
    {
        GameAction.levelComplete += SetNextLevel;
        GameAction.openNextLevel += OpenNextLevel;
        GameAction.restartLevel += RestartLevel;
        DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
        GameAction.levelComplete -= SetNextLevel;
        GameAction.openNextLevel -= OpenNextLevel;
        GameAction.restartLevel -= RestartLevel;
    }

    [ContextMenu("OpenNextLevel")]
    void OpenNextLevel() => StartCoroutine(OpenScene(PlayerPrefs.GetInt(Keys.currentScene)));
    void RestartLevel() => StartCoroutine(OpenScene(SceneManager.GetActiveScene().buildIndex));
    
    IEnumerator OpenScene(int sceneIndex)
    {
        GameAction.showTransitionScreen?.Invoke();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;
        yield return new WaitForSeconds(.38f);
        asyncLoad.allowSceneActivation = true;
    }

    void SetNextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt(Keys.levelCompleted);
        PlayerPrefs.SetInt(Keys.levelCompleted, currentLevel + 1);
        
        GAEvents.LevelComplete(currentLevel);
        
        int nextScene = PlayerPrefs.GetInt(Keys.currentScene) + 1;
        if (nextScene >= SceneManager.sceneCountInBuildSettings || nextScene <= 1) nextScene = 2;
        PlayerPrefs.SetInt(Keys.currentScene, nextScene);
    }
}

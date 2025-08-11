using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader
{
    public IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            float progress = asyncLoad.progress;
            Debug.Log($"Loading: {progress * 100}%");
            yield return null;
        }
    }

    public void LoadScene(string sceneName)
    {
        LoadSceneAsync(sceneName);
    }
}
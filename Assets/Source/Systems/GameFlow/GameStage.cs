using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameStage : MonoBehaviour
{
    protected void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    protected void LoadSceneAsync(string sceneName, Action onLoad)
    {
        StartCoroutine(sceneName, onLoad);
    }

    IEnumerator LoadSceneAsyncCoroutine(string sceneName, Action onLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        onLoad?.Invoke();
    }
    
    public abstract void OnStateEnter();

    public virtual void OnStateExit()
    {
        
    }
}

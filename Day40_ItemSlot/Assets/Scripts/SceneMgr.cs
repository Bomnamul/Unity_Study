using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr instance;
    public event Action OnBeginLoad;
    public event Action OnLoadCompleted;
    public event Action<float> OnProgress;
    public string prevScene;

    bool isLoading = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LoadScene(string nextScene)
    {
        if (!isLoading)
        {
            StartCoroutine(AsynchronousLoad(nextScene));
        }
    }

    IEnumerator AsynchronousLoad(string Scene)
    {
        isLoading = true;
        prevScene = SceneManager.GetActiveScene().name;
        OnBeginLoad?.Invoke();

        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(Scene); // AsyncOperation: Coroutine으로 생각
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            //float progress = Mathf.Clamp01(ao.progress / 0.9f);
            //OnProgress?.Invoke(progress);

            int i = 0;
            while (i <= 10)
            {
                float progress = Mathf.Clamp01(i / 10f);
                OnProgress?.Invoke(progress);
                yield return new WaitForSeconds(0.01f);
                i++;
            }

            // Loading completed
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
                isLoading = false;
                OnLoadCompleted?.Invoke();
            }

            yield return null;
        }
    }
}

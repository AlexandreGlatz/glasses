using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image LoadingBarFill;

    public void LoadScene(int sceneId)
    {

        StartCoroutine(LoadSceneAsync(sceneId));
    }

    public IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / .9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }
}
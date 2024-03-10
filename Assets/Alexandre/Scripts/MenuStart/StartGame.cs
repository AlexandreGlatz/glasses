using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public LoadingScreen loadingScreen;

    public void GameStart()
    {
        loadingScreen.LoadScene(1);
    }

    public void GameStop()
    {
        Application.Quit();
    }
}

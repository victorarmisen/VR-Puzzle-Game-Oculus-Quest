using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int GameSceneBuildIndex = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(GameSceneBuildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
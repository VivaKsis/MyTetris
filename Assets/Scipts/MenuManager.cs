using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    public string loadGame;
    public string loadScores;
    public void StartGame()
    {
        SceneManager.LoadScene(loadGame);
    }

    public void ScoreTable()
    {
        SceneManager.LoadScene(loadScores);
    }

    public void ExitGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }
}

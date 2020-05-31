using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScoreManager : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }
}

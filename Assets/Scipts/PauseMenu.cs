using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public Text t;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Pause))
        {
            if (GameIsPaused)
            {
                StartCoroutine(Resume());
            }
            else
            {
                Pause();
            }
        }
    }

    IEnumerator Resume()
    {
        yield return StartCoroutine(Text321.set321(t));
        yield return StartCoroutine(Text321.set321Resume(t));
        Time.timeScale = Scoring.speed;
        GameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        t.text = "PAUSE";
    }
}

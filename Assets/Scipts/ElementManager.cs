using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElementManager : MonoBehaviour
{
    public int a;

    Transform currentFigureTransform;
    GameObject go;

    public Text textScore;
    public Text t;

    private int score;
    private int scorenumber;

    public string loadPlayerName;

    bool isValidGridPos()
    {
        foreach (Transform child in currentFigureTransform)
        {
            Vector2 v = PlayfieldManager.roundVec2(child.position);
            // Not inside Border?
            if (!PlayfieldManager.insideBorder(v))
            {
                return false;

            }
            // Block isn't on an empty spot
            if (PlayfieldManager.grid[(int)v.x, (int)v.y] != null &&
                PlayfieldManager.grid[(int)v.x, (int)v.y].parent != currentFigureTransform)
            {
                Debug.Log("Check if it was just spawned");
                // Check if it was just spawned
                if ((int)v.y >= 17)
                {
                    Debug.Log("GAME OVER");
                    Destroy(currentFigureTransform.gameObject);

                    int b;
                    if (scorenumber == 0)
                        b = -1;
                    else
                        b = CheckScoreBoard.isScoreToBeInBoard(scorenumber);
                    if (b != -1)
                    {
                        SceneManager.LoadScene(loadPlayerName);
                    }
                    else
                    {
                        Application.Quit();
                        EditorApplication.isPlaying = false;
                    }
                }
                return false;
            }
        }
        return true;
    }

    void Start()
    {
        updateCurrentTransform();
        score = 0;
        Scoring.SetScore(score, textScore);
    }

    void updateCurrentTransform()
    {
        go = GameObject.Find("Current Figure");
        if (go != null)
        {
            currentFigureTransform = go.transform;
        }
        else
        {
            Debug.Log("Figure not found");
        }
    }

    void updateGrid()
    {
        for (int y = 0; y < 20; ++y)
            for (int x = 0; x < 10; ++x)
                if (PlayfieldManager.grid[x, y] != null)
                    if (PlayfieldManager.grid[x, y].parent == currentFigureTransform)
                        PlayfieldManager.grid[x, y] = null;

        foreach (Transform child in currentFigureTransform)
        {
            Vector2 v = PlayfieldManager.roundVec2(child.position);
            PlayfieldManager.grid[(int)v.x, (int)v.y] = child;
        }
    }

    void FreeChildren()
    {
        currentFigureTransform.DetachChildren();
        Destroy(currentFigureTransform.gameObject);
    }

    float lastFall = 0;
    int fallingIndex = 1;
    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentFigureTransform == null)
                updateCurrentTransform();
            currentFigureTransform.position += new Vector3(-1, 0, 0);

            if (isValidGridPos())
                updateGrid();
            else
                currentFigureTransform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentFigureTransform == null)
                updateCurrentTransform();
            currentFigureTransform.position += new Vector3(1, 0, 0);

            if (isValidGridPos())
                updateGrid();
            else
                currentFigureTransform.position += new Vector3(-1, 0, 0);
        }

        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (currentFigureTransform == null)
                updateCurrentTransform();
            currentFigureTransform.Rotate(0, 0, -90);

            if (isValidGridPos())
                updateGrid();
            else
            {
                currentFigureTransform.Rotate(0, 0, 90);
                Debug.Log(currentFigureTransform.position);
            }
        }

        // Move Downwards and Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - lastFall >= 1))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                fallingIndex++;

            for (int i = 0; i < fallingIndex; ++i)
            {
                if (currentFigureTransform == null)
                    updateCurrentTransform();

                currentFigureTransform.position += new Vector3(0, -1, 0);
                if (isValidGridPos())
                {
                    updateGrid();
                }
                else
                {
                    currentFigureTransform.position += new Vector3(0, 1, 0);

                    Sound.playLanding();

                    score = PlayfieldManager.deleteFullRows();
                    if (score > 0)
                        scorenumber = Scoring.SetScore(score, textScore);

                    FreeChildren();

                    FindObjectOfType<Spawner>().SpawnElement();

                    updateCurrentTransform();

                    break;
                }
            }
            fallingIndex = 1;
            lastFall = Time.time;
        }
    }


}

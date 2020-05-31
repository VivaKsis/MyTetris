using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    private static int score;

    public static float speed = 1f;

    // Plusing the score depending on the amount of lines that were created
    public static int SetScore(int amountLines, Text t)
    {
        switch (amountLines)
        {
            case 1:
                score += 5;
                break;
            case 2:
                score += 15;
                break;
            case 3:
                score += 45;
                break;
            case 4:
                score += 150;
                break;
            case 0:
                score = 0;
                break;
            default:
                break;
        }
        t.text = "Score: " + score.ToString();
        SetSpeed();
        return score;
    }


    // Speeding up the game after reaching score
    public static void SetSpeed()
    {
        if (score >= 200)
            speed = 3f;
        else if (score >= 500)
            speed = 5f;
        else if (score >= 1000)
            speed = 10f;
        Time.timeScale = speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CheckScoreBoard : MonoBehaviour
{
    // Buffer for txt file
    private static string[] scoreBoard = new string[20];

    public InputField player;
    public string playerName;

    private static int num; // The number of the line to be moved down
    private static int finalscore;

    public string loadScoreBoard; // Name of the scene of scoreBoard

    static string filepath = "Assets/txt/ScoreTable.txt";
    static string folderpath = "Assets/txt/";

    public static int isScoreToBeInBoard(int score)
    {
        int number;

        if (!File.Exists(filepath))
        {
            Debug.Log("File must be created");
            createNewScoreBoard();
        }
        StreamReader reader = new StreamReader(filepath);
        if (reader != null) Debug.Log("File was opened");
        for (int i = 0; !reader.EndOfStream; i++)
        {
            scoreBoard[i] = reader.ReadLine();
        }

        //checking the lines [19], [17], [15]...
        finalscore = score;
        Debug.Log("looking for right place");
        for (num = 19; num >= 1; num -= 2)
        {
            bool isParsable = Int32.TryParse(scoreBoard[num], out number);
            if (isParsable)
            {
                if (number < score)
                {
                    if (num == 1) //First place!
                    {
                        //Debug.Log("First Place");
                        return num;
                    }
                    else //Check the higher score
                    {
                        isParsable = Int32.TryParse(scoreBoard[num - 2], out number);
                        if (isParsable)
                        {
                            if (number > score) // Must replace lower scores
                            {
                                //Debug.Log(num);
                                return num;
                            }
                            else continue; // Must check higher
                        }
                        else Debug.Log("Cannot be parsed");
                    }
                }
                else
                {
                    if (number == score && num != 19) // Scores are equal so we move lower if possible
                    {
                        //Debug.Log((num + 2));
                        return num + 2;
                    }
                    else // Score is too small for board
                    {
                        Debug.Log(number + " is bigger than " + score);
                        return -1;
                    }
                }
            }
            else Debug.Log("Cannot be parsed");
        }
        Debug.Log("Something clearly wrong");
        return -1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            playerName = player.text;
            //Debug.Log(playerName);
            writeInBuffer();
        }
    }

    void writeInBuffer()
    {
        int i = 19, j = 17;
        while (i != num)
        {
            scoreBoard[i] = scoreBoard[j];
            scoreBoard[i - 1] = scoreBoard[j - 1];
            i -= 2;
            j -= 2;
        }
        scoreBoard[i - 1] = playerName;
        scoreBoard[i] = finalscore.ToString();
        writeInFile();
    }

    void writeInFile()
    {
        StreamWriter writer = new StreamWriter(filepath, false);
        for (int i = 0; i < 20; i++)
        {
            writer.WriteLine(scoreBoard[i]);
        }
        writer.Close();

        SceneManager.LoadScene(loadScoreBoard);

    }

    public static void createNewScoreBoard()
    {
        if (!Directory.Exists(folderpath))
            Directory.CreateDirectory(folderpath);
        StreamWriter sw = File.CreateText(filepath);
        for (int i = 0; i < 10; i++)
        {
            sw.WriteLine("Name");
            sw.WriteLine("0");
        }
        sw.Close();
    }
}


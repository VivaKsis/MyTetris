using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class PrintScoreBoard : MonoBehaviour
{
    public Text[] scoreText;

    void Start()
    {
        ReadScoreBoard();
    }

    void ReadScoreBoard()
    {
        string path = "Assets/txt/ScoreTable.txt";

        string buff1, buff2;

        StreamReader reader = new StreamReader(path);
        if ((!File.Exists(path)))
        {
            Debug.Log("File must be created");
            CheckScoreBoard.createNewScoreBoard();
        }

        for (int i=0; !reader.EndOfStream; i++)
        {
            buff1 = reader.ReadLine();
            buff2 = reader.ReadLine();
            buff1 = (i+1).ToString() + ". " + buff1 + "......" + buff2;
            scoreText[i].text = buff1;
        }
        reader.Close();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    /*private string[] players = new string[] { "Player 1", "Player2", "Player 3", "Player 4" };
    private int[] highscoreValues;*/
    public static Timer instanceT = null;
    public LevelManager manager;
    private bool active = true;
    public Text counterText;
    public Text[] scoreBoard;
    private string levelInstructions = "nlsdsf";
    private float timer = 60f;
    private int[] score;
    private int[] dScore;
    private int player1Score =1;
    private int player2Score =2;
    private int player3Score =3;
    private int player4Score =4;
    // Use this for initialization
    void Awake()
    {
        if (instanceT == null)
            instanceT = this;

        else if (instanceT != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        /* highscoreValues = new int[scoreBoard.Length];
         for (int x = 0; x < scoreBoard.Length; x++)
         {
             highscoreValues[x] = PlayerPrefs.GetInt("highScoreValues" + x);
         }
         drawScores();*/
        score = new int[] { player1Score, player2Score, player3Score, player4Score };
        displayScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
            counterText.text = levelInstructions + "\n" + (int)timer;
        else
            counterText.text = levelInstructions;
        timer = timer - Time.deltaTime;
        if (timer < 0 && active == true)
        {
            manager.loadNextLevel();
        }

    //displayScore();
    }
    public void updateManager(LevelManager newM)
    {
        manager = newM;
    }

    public void setInstuctions(string s)
    {
        levelInstructions = s;
    }
    public void setTimer(float t)
    {
        timer = t;
    }
    public void playerDied()
    {
        timer = timer / 2;
    }
    public void timerActive(bool t)
    {
        active = t;
    }
    private void drawScores()
    {
        for(int x = 0; x<scoreBoard.Length; x++)
        {
            //scoreBoard[x].text = players[x] + ":" + highscoreValues[x].ToString();
        }
    }
   /* private void saveScores()
    {
        for (int x = 0; x < scoreBoard.Length; x++)
        {
            PlayerPrefs.SetInt("highScoreValues" + x, highscoreValues[x]);
            PlayerPrefs.SetString("highScoreNames" + x, players[x]);
        }
    }*/
    public void runeGet(string name , int value)
    {
        switch (name)
        {
            case "P1":
                score[0] = score[0] +10*value;
                displayScore();
                break;
            case "P2":
                score[1] = score[1] +10 * value;
                displayScore();
                break;
            case "P3":
                score[2] = score[2] + 10 * value;
                displayScore();
                break;
            case "P4":
                score[3] = score[3] + 10 * value;
                displayScore();
                break;
        }
    }
    public void ghostSteal(string ghostName, string playerName, int value)
    {
        switch (playerName)
        {
            case "P1":
                score[0] = score[0] - 10 * value;
                runeGet(ghostName, value);
                break;
            case "P2":
                score[1] = score[1] - 10 * value;
                runeGet(ghostName, value);
                break;
            case "P3":
                score[2] = score[2] - 10 * value;
                runeGet(ghostName, value);
                break;
            case "P4":
                score[3] = score[3] - 10 * value;
                runeGet(ghostName, value);
                break;
        }
    }

    private void displayScore()
    {
        dScore = new int[] { score[0], score[1], score[2], score[3] };
        Array.Sort(dScore);
        int[] switchA = new int[] { dScore[0]%10, dScore[1]%10, dScore[2]%10, dScore[3]%10 };
        for (int x =0; x<score.Length; x++)
        {
            switch  (switchA[x])
            {
                case 1:
                    scoreBoard[3-x].text = "Player 1: " + (score[0]-1)/10;
                    break;
                case 2:
                    scoreBoard[3-x].text = "Player 2: " + (score[1]-2)/10;
                    break;
                case 3:
                    scoreBoard[3-x].text = "Player 3: " + (score[2]-3)/10;
                    break;
                case 4:
                    scoreBoard[3-x].text = "Player 4: " + (score[3]-4)/10;
                    break;
            }
        }


    }

    public float getTimer()
    {
        return timer;
    }

    public void setStartingPositions()
    {
        Debug.Log("setPositions enter");
        var scoreSorted = score;
        Array.Sort(scoreSorted);
        int[] playerID = new int[] { dScore[0] % 10, dScore[1] % 10, dScore[2] % 10, dScore[3] % 10 };
        GameObject.FindGameObjectWithTag("Starting Positions").GetComponent<StartingPositions>().setPositions(playerID);
    }
    public void doAlch(string player)
    {
        print(manager.getAlive());
        switch (manager.getAlive())
        {
            case 4:
                runeGet(player, 30);
                break;
            case 3:
                runeGet(player, 20);
                break;
            case 2:
                runeGet(player, 10);
                break;
            case 1:
                manager.loadNextLevel();
                break;
        }

    } 
}
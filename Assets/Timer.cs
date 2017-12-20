using System.Collections;
using UnityEngine.SceneManagement;
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
    public GameObject[] scoreIcons;
    private bool player1s = true, player2s = true, player3s = true, player4s = true;
    private List<bool> simpleControls = null;
    private string lastLevel = "Nothing";
    bool stopTimer = false;
    private GameObject levelkeeper;
    private bool christmasmiracle = false;
    private float saveTime;
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
        //score = new int[] { player1Score, player2Score, player3Score, player4Score };
        score = SetUpScore();
        dScore = new int[0];
        displayScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && SceneManager.GetActiveScene().name == "Level7(BigxSmaller)")
        {

            if (timer > 10f)
                counterText.text = levelInstructions + "\n" + ((int)timer - 2);
            else if(timer >0)
                counterText.text = levelInstructions + "\n" + "<color=#ff0000ff>" + "<size=40>" + ((int)timer - 2) + "</size> </color>";
            else if (timer < 0)
            {
                counterText.text = levelInstructions + "\n" + "<color=#ff0000ff>" + "<size=40>" + "0" + "</size> </color>";
            }
        }
        else if (active)
        {
            if(timer>10f)
            counterText.text = levelInstructions + "\n" + (int)timer;
            else
                counterText.text = levelInstructions + "\n" + "<color=#ff0000ff>" + "<size=40>"+(int)timer+"</size> </color>";
        }
        else
            counterText.text = levelInstructions;
        if (!stopTimer)
        {
            timer = timer - Time.deltaTime;
            if (timer < 0 && active == true)
            {
                manager.loadNextLevel();
            }
        }
        dScore = copyArray(score);
        Array.Sort(dScore);
        int[] playerID = getPlayerID(score);
        if (!(dScore[dScore.Length-2] > dScore[dScore.Length-1]))
        {
            /*GameObject.Find("P1").transform.GetChild(7).gameObject.SetActive(false);
            GameObject.Find("P2").transform.GetChild(7).gameObject.SetActive(false);
            GameObject.Find("P3").transform.GetChild(7).gameObject.SetActive(false);
            GameObject.Find("P4").transform.GetChild(7).gameObject.SetActive(false);*/
            for (int i = 0; i <= playerID.Length - 1; i++)
                GameObject.Find("P" + (i + 1)).transform.GetChild(7).gameObject.SetActive(false);
        }
        else { 
            switch (playerID[3])
            {
                case 1:
                    GameObject.Find("P1").transform.GetChild(7).gameObject.SetActive(true);
                    GameObject.Find("P2").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P3").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P4").transform.GetChild(7).gameObject.SetActive(false);
                    break;
                case 2:
                    GameObject.Find("P2").transform.GetChild(7).gameObject.SetActive(true);
                    GameObject.Find("P1").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P3").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P4").transform.GetChild(7).gameObject.SetActive(false);
                    break;
                case 3:
                    GameObject.Find("P3").transform.GetChild(7).gameObject.SetActive(true);
                    GameObject.Find("P2").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P1").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P4").transform.GetChild(7).gameObject.SetActive(false);
                    break;
                case 4:
                    GameObject.Find("P4").transform.GetChild(7).gameObject.SetActive(true);
                    GameObject.Find("P2").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P3").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P1").transform.GetChild(7).gameObject.SetActive(false);
                    break;
                default:
                    GameObject.Find("P1").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P2").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P3").transform.GetChild(7).gameObject.SetActive(false);
                    GameObject.Find("P4").transform.GetChild(7).gameObject.SetActive(false);
                    break;
            }
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
        dScore = copyArray(score);
        Array.Sort(dScore);
        int[] switchA = getPlayerID(dScore);
        for (int x =0; x<score.Length; x++)
        {
            switch  (switchA[x])
            {
                case 1:
                    scoreBoard[3-x].text = "Player 1: " + (score[0]-1)/10;
                    scoreIcons[3].transform.position = new Vector3(scoreIcons[3].transform.position.x, scoreBoard[3 - x].transform.position.y, scoreIcons[3].transform.position.z);
                    break;
                case 2:
                    scoreBoard[3-x].text = "Player 2: " + (score[1]-2)/10;
                    scoreIcons[1].transform.position = new Vector3(scoreIcons[1].transform.position.x, scoreBoard[3 - x].transform.position.y, scoreIcons[1].transform.position.z);
                    break;
                case 3:
                    scoreBoard[3-x].text = "Player 3: " + (score[2]-3)/10;
                    scoreIcons[0].transform.position = new Vector3(scoreIcons[0].transform.position.x, scoreBoard[3 - x].transform.position.y, scoreIcons[0].transform.position.z);
                    break;
                case 4:
                    scoreBoard[3-x].text = "Player 4: " + (score[3]-4)/10;
                    scoreIcons[2].transform.position = new Vector3(scoreIcons[2].transform.position.x, scoreBoard[3 - x].transform.position.y, scoreIcons[2].transform.position.z);
                    break;
            }
        }


    }

    public float getTimer()
    {
        return timer;
    }
    public string getLastLevel()
    {
        return lastLevel;
    }
    public void setStartingPositions(string s)
    {
        var scoreSorted = copyArray(score);
        Array.Sort(scoreSorted);
        int[] playerID = getPlayerID(scoreSorted);
        if (s == "Speedrun")
            GameObject.FindGameObjectWithTag("Starting Positions").GetComponent<StartingPositions>().setPositions(playerID);
        else if (s == "Credit")
            GameObject.FindGameObjectWithTag("Starting Positions").GetComponent<StartingPositions>().setCreditPositions(playerID);
    }
    public void doGrimoire(string player, int value)
    {

        switch (player)
        {
            case "P1":
                if (player1s)
                {
                    runeGet(player, value);
                    player1s = false;
                }
                break;
            case "P2":
                if (player2s)
                {
                    runeGet(player, value);
                    player2s = false;
                }
                break;
            case "P3":
                if (player3s)
                {
                    runeGet(player, value);
                    player3s = false;
                }
                break;
            case "P4":
                if (player4s)
                {
                    runeGet(player, value);
                    player4s = false;
                }
                break;
        }

    }
    public void setLastLevel(string s)
    {
        lastLevel = s;
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

    public void setGrimoire()
    {
        player1s = true;
        player2s = true;
        player3s = true;
        player4s = true;
        var scoreSorted = new int[0];
        scoreSorted = copyArray(score);
        Array.Sort(scoreSorted);
        int playerid = scoreSorted[scoreSorted.Length-1] % 10;
        GameObject.Find("GrimoireGiver").GetComponent<GrimoireGiver>().giveGrimoire(playerid);
    }

    public void setControls()
    {
        if (simpleControls == null)
            return;
        for(int i = 0; i <= score.Length-1; i++)
        {
            GameObject.Find("P" + (i+1)).GetComponent<Controller2D>().SimpleControls = simpleControls[i];
        }
    }

    public void SaveControls(List<bool> bl)
    {
        simpleControls = bl;
    }

    /*public int[] getSortedArray()
    {
        
    }*/

    private int[] SetUpScore()
    {
        var size = GameObject.Find("UI Camera").GetComponent<GameManager>().connectedControllers;
        int[] arr = new int[size];
        for(int i = 1; i <= size; i++)
        {
            arr[i - 1] = i;
        }
        return arr;
        
    }

    private int[] getPlayerID(int[] inarr)
    {
        int[] arr = new int[inarr.Length];
        for(int i = 0; i <= inarr.Length - 1; i++)
        {
            arr[i] = inarr[i] % 10;
        }
        return arr;
    }

    private int[] copyArray(int[] arr)
    {
        int[] copy = new int[arr.Length];
        for (int i = 0; i <= arr.Length - 1; i++)
            copy[i] = arr[i];

        return copy;
    }

    public void addPlayer(int size)
    {
        size -= score.Length;
        if (size <= 0)
            return;

        int[] arr = new int[score.Length + size];
        for(int i = 0; i < arr.Length; i++)
        {
            if (i >= score.Length)
            {
                arr[i] = i + 1;
            }
            else
            {
                arr[i] = score[i];
            }
        }

        score = arr;
        displayScore();
    }

    public void startTimer()
    {
        stopTimer = false;
    }

    public void StopTimer()
    {
        stopTimer = true;
    }
    public void setchristmasmiracle(bool t)
    {
        christmasmiracle = t;
    }
    public void setlevelKeeper(GameObject keep)
    {
        levelkeeper = keep;
    }
    public bool getchristmasmiracle()
    {
        return christmasmiracle;
    }
    public void activatelevelKeeper(bool t)
    {
        levelkeeper.SetActive(t);
        if (!t)
        {
            saveTime = timer;
        }
        else
        {
            timer = saveTime;
        }
    }



}
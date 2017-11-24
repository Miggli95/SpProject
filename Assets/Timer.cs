using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /*private string[] players = new string[] { "Player 1", "Player2", "Player 3", "Player 4" };
    private int[] highscoreValues;*/
    public static Timer instanceT = null;
    private bool active = true;
    public Text counterText;
    public Text[] scoreBoard;
    private string levelInstructions = "nlsdsf";
    private float timer = 60f;
    private int player1Score;
    private int player2Score;
    private int player3Score;
    private int player4Score;
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
    }

    // Update is called once per frame
    void Update()
    {

            counterText.text = levelInstructions + "\n" + (int)timer;
        timer = timer - Time.deltaTime;
        
       
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

}
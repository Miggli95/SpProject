using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance = null;
    public GameObject timerText;
    private int level = 0;
    private string[] levelorder = { "CharacterControllerDevelopmentScene", "Level4(24x16) 1" };
    private GameObject[] players;
    private int livingPlayers;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitGame();
    }


    void InitGame()
    {
        startLevel();
        //do game start stuff

    }

    public void startLevel()
    {

        //loadlevelstuff
        players = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        players = GameObject.FindGameObjectsWithTag("Player");
        livingPlayers = players.Length;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Hub(24x16)":

                timerText.GetComponent<Timer>().setInstuctions("Press A to Jump" + "\n" + "Press X to Pick Up" + "\n" + "Press Y to interact");
                timerText.GetComponent<Timer>().timerActive(false);
                break;
            case "CharacterControllerDevelopmentScene":
                timerText.GetComponent<Timer>().timerActive(true);
                timerText.GetComponent<Timer>().setInstuctions("Dance Party");
                timerText.GetComponent<Timer>().setTimer(60f);
                break;
            case "Level4(24x16) 1":
                timerText.SetActive(true);
                timerText.GetComponent<Timer>().timerActive(true);
                timerText.GetComponent<Timer>().setInstuctions("Drink the potions!");
                timerText.GetComponent<Timer>().setTimer(60f);
                break;
            case "Level5(24x16)":
                timerText.SetActive(true);
                timerText.GetComponent<Timer>().timerActive(true);
                timerText.GetComponent<Timer>().setInstuctions("We Spinnin");
                timerText.GetComponent<Timer>().setTimer(60f);
                break;
        }
        if (SceneManager.GetActiveScene().name == "Level5(24x16)")
        {
            timerText.GetComponent<Timer>().setInstuctions("We Spinnin");
        }

    }

    void Update()
    {
        int i = 0;
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Controller2D>().getAlive())
            {
                i++;
            }
        }
        if(i < livingPlayers)
        {
            timerText.GetComponent<Timer>().playerDied();
            livingPlayers = i;
        }
        if (i == 1)
        {
            print("you did die");
            loadNextLevel();
        }
    }
    public void loadNextLevel()
    {
        level++;
        SceneManager.LoadScene(levelorder[level]);
    }
    public GameObject[] getPlayers()
    {
        return players;
    }
}
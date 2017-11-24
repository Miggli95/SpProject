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
    private GameObject deadPlayer;

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
                //timerText.GetComponent<Timer>().timerActive(false);
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
            case "ControllTestLevel":
                timerText.SetActive(true);
                timerText.GetComponent<Timer>().timerActive(true);
                timerText.GetComponent<Timer>().setInstuctions("Feed the bear!");
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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Hub(24x16)");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Level4(24x16) 1");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("ControllTestLevel");
        }
        int i = 0;
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Controller2D>().getAlive())
            {
                i++;
            }
            else
            {
                deadPlayer = p;
            }
        }
        if(i < livingPlayers)
        {
            timerText.GetComponent<Timer>().playerDied();
            livingPlayers = i;
            //doscoreboardshit(deadPlayer)
        }
        if (i == 1)
        {
            print("you did die");
            loadNextLevel();
        }
    }
    public void loadNextLevel()
    {

       

        if (SceneManager.GetActiveScene().name == "Hub(24x16)")
        {
            SceneManager.LoadScene("ControllTestLevel");
        }
        if (SceneManager.GetActiveScene().name == "Level4(24x16) 1")
        {
            SceneManager.LoadScene("ControllTestLevel");
        }
        if (SceneManager.GetActiveScene().name == "ControllTestLevel")
        {
            SceneManager.LoadScene("Level4(24x16) 1");
        }

        timerText.GetComponent<Timer>().setInstuctions("");
        startLevel();
    }
    public GameObject[] getPlayers()
    {
        return players;
    }
}
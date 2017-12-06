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
    List<GameObject> players;
    private int livingPlayers;
    private GameObject deadPlayer;
    private bool dontcheckplayers = true;

    void Awake()
    {

        players = new List<GameObject> { };
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++) {
            players.Add(GameObject.FindGameObjectsWithTag("Player")[i]);
        }
        InitGame();
    }


    void InitGame()
    {
        startLevel();
        //do game start stuff

    }

    public void startLevel()
    {
        players.Clear();
        timerText = GameObject.FindGameObjectsWithTag("UICamera")[0];
        timerText.GetComponent<Timer>().updateManager(this);
        players = new List<GameObject> { };
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            players.Add(GameObject.FindGameObjectsWithTag("Player")[i]);
        }
        livingPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
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
            case "Level4(36x18) 1":
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
                timerText.GetComponent<Timer>().setStartingPositions();
                break;
            case "Level7(BigxSmaller)":
                timerText.SetActive(true);
                timerText.GetComponent<Timer>().timerActive(true);
                timerText.GetComponent<Timer>().setInstuctions("Keep the grimoire away from the others!");
                timerText.GetComponent<Timer>().setTimer(60f);
                timerText.GetComponent<Timer>().setGrimoire();
                break;
        }
        dontcheckplayers = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            loadNextLevel();
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
        if (!dontcheckplayers)
        {
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
            if (i < livingPlayers)
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
    }
    public void loadNextLevel()
    {

        players.Clear();
        dontcheckplayers = true;

        if (SceneManager.GetActiveScene().name == "Hub(24x16)")
        {
            SceneManager.LoadScene("Level4(36x18) 1");
        }
        if (SceneManager.GetActiveScene().name == "Level4(36x18) 1")
        {
            SceneManager.LoadScene("ControllTestLevel");
        }
        if (SceneManager.GetActiveScene().name == "ControllTestLevel")
        {
            SceneManager.LoadScene("Level7(BigxSmaller)");
        }
        if(SceneManager.GetActiveScene().name== "Level7(BigxSmaller)")
        {
            SceneManager.LoadScene("Hub(24x16)");
        }

        timerText.GetComponent<Timer>().setInstuctions("");

        startLevel();
    }
    public List<GameObject> getPlayers()
    {
        return players;
    }
    public int getAlive()
    {
        return livingPlayers;
    }
}
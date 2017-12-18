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
    public GameObject Level2Lock;
    public GameObject Level2Menu;
    public GameObject Level3Lock;
    public GameObject Level3Menu;

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
        timerText.GetComponent<Timer>().setControls();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Hub(24x16)":
                switch (timerText.GetComponent<Timer>().getLastLevel())
                {
                    case "Nothing":
                        timerText.GetComponent<Timer>().setInstuctions("Welcome to Leaping Lemmings");
                        timerText.GetComponent<Timer>().timerActive(false);
                        break;
                    case "Level1":
                        timerText.GetComponent<Timer>().setInstuctions("Well done feeding the bear, now finish the job");
                        timerText.GetComponent<Timer>().timerActive(false);
                        Level2Lock.SetActive(false);
                        Level2Menu.SetActive(true);
                        break;
                    case "Level2":
                        timerText.GetComponent<Timer>().setInstuctions("The bear sated now you have to finish the dark ritual");
                        timerText.GetComponent<Timer>().timerActive(false);
                        Level2Lock.SetActive(false);
                        Level2Menu.SetActive(true);
                        Level3Lock.SetActive(false);
                        Level3Menu.SetActive(true);
                        break;
                    case "Level3":
                        timerText.GetComponent<Timer>().setInstuctions("");
                        timerText.GetComponent<Timer>().timerActive(false);
                        Level2Lock.SetActive(false);
                        Level2Menu.SetActive(true);
                        Level3Lock.SetActive(false);
                        Level3Menu.SetActive(true);
                        break;
                }
                
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
                timerText.GetComponent<Timer>().setStartingPositions("Speedrun");
                break;
            case "Level7(BigxSmaller)":
                timerText.SetActive(true);
                timerText.GetComponent<Timer>().timerActive(true);
                timerText.GetComponent<Timer>().setInstuctions("Keep the grimoire away from the others!");
                timerText.GetComponent<Timer>().setTimer(62f);
                timerText.GetComponent<Timer>().setGrimoire();
                dontcheckplayers = true;
                return;
            case "Credit":
                timerText.SetActive(true);
                timerText.GetComponent<Timer>().timerActive(false);
                timerText.GetComponent<Timer>().setInstuctions("");
                timerText.GetComponent<Timer>().setStartingPositions("Credit");
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
            if (SceneManager.GetActiveScene().name != "Level5(24x16) 2")
                SceneManager.LoadScene("Level5(24x16) 2");
            else
                SceneManager.LoadScene("Hub(24x16)");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (timerText.GetComponent<Timer>().getLastLevel() == "Level2")
                timerText.GetComponent<Timer>().setLastLevel("Level3");
            if (timerText.GetComponent<Timer>().getLastLevel() == "Level1")
                timerText.GetComponent<Timer>().setLastLevel("Level2");
            if (timerText.GetComponent<Timer>().getLastLevel()== "Nothing")
                timerText.GetComponent<Timer>().setLastLevel("Level1");

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
        saveSimpleControls();
        players.Clear();
        dontcheckplayers = true;

        if (SceneManager.GetActiveScene().name == "Hub(24x16)")
        {
            SceneManager.LoadScene("Level8");
        }
        if (SceneManager.GetActiveScene().name == "Level8")
        {
            SceneManager.LoadScene("Level9");
        }
        if (SceneManager.GetActiveScene().name == "Level9")
        {
            SceneManager.LoadScene("Level7(BigxSmaller)");
        }
        if(SceneManager.GetActiveScene().name== "Level7(BigxSmaller)")
        {
            SceneManager.LoadScene("Credit");
        }
        if (SceneManager.GetActiveScene().name == "Credit")
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

    private void saveSimpleControls()
    {
        bool[] simpleControls = new bool[4];
        foreach(GameObject g in players)
        {
            switch (g.name)
            {
                case "P1":
                    simpleControls[0] = g.GetComponent<Controller2D>().SimpleControls;
                    break;
                case "P2":
                    simpleControls[1] = g.GetComponent<Controller2D>().SimpleControls;
                    break;
                case "P3":
                    simpleControls[2] = g.GetComponent<Controller2D>().SimpleControls;
                    break;
                case "P4":
                    simpleControls[3] = g.GetComponent<Controller2D>().SimpleControls;
                    break;
            }
        }
        List<bool> b = new List<bool> { simpleControls[0], simpleControls[1], simpleControls[2], simpleControls[3] };
        timerText.GetComponent<Timer>().SaveControls(b);
    }
}
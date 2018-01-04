using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance = null;
    public Timer timerText;
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
    private GameObject levelKeeper;
    private Timer timmy;
    private float pressJtimer = 3f;
    void Start()
    {

        RecountPlayers();
        InitGame();
        levelKeeper = GameObject.Find("LevelKeeper");
        timmy = GameObject.Find("UI Camera").GetComponent<Timer>();
        
    }


    void InitGame()
    {
        startLevel();
        //do game start stuff

    }

    public void RecountPlayers()
    {
        timerText = GameObject.FindGameObjectsWithTag("UICamera")[0].GetComponent<Timer>();
        players = new List<GameObject> { };
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            players.Add(GameObject.FindGameObjectsWithTag("Player")[i]);
        }
    }

    public void startLevel()
    {
        players.Clear();
        
        timerText.updateManager(this);
        players = new List<GameObject> { };
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            players.Add(GameObject.FindGameObjectsWithTag("Player")[i]);
        }
        livingPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        timerText.setControls();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Hub(24x16)":
                switch (timerText.getLastLevel())
                {
                    case "Nothing":
                        timerText.setInstuctions("Welcome to Leaping Lemmings");
                        timerText.timerActive(false);
                        break;
                    case "Level1":
                        timerText.setInstuctions("Well done feeding the bear, now finish the job");
                        timerText.timerActive(false);
                        Level2Lock.SetActive(false);
                        Level2Menu.SetActive(true);
                        break;
                    case "Level2":
                        timerText.setInstuctions("The bear sated now you have to finish the dark ritual");
                        timerText.timerActive(false);
                        Level2Lock.SetActive(false);
                        Level2Menu.SetActive(true);
                        Level3Lock.SetActive(false);
                        Level3Menu.SetActive(true);
                        break;
                    case "Level3":
                        timerText.setInstuctions("");
                        timerText.timerActive(false);
                        Level2Lock.SetActive(false);
                        Level2Menu.SetActive(true);
                        Level3Lock.SetActive(false);
                        Level3Menu.SetActive(true);
                        break;
                }
                
                break;
            case "CharacterControllerDevelopmentScene":
                timerText.timerActive(true);
                timerText.setInstuctions("Dance Party");
                timerText.setTimer(60f);
                break;
            case "Level4(36x18) 1":
                timerText.gameObject.SetActive(true);
                timerText.timerActive(true);
                timerText.setInstuctions("Drink the potions!");
                timerText.setTimer(60f);
                break;
            case "Level5(24x16)":
                timerText.gameObject.SetActive(true);
                timerText.timerActive(true);
                timerText.setInstuctions("We Spinnin");
                timerText.setTimer(60f);
                break;
            case "ControllTestLevel":
                timerText.gameObject.SetActive(true);
                timerText.timerActive(true);
                timerText.setInstuctions("Feed the bear!");
                timerText.setTimer(60f);
                timerText.setStartingPositions("Speedrun");
                break;
            case "Level8":
                timerText.gameObject.SetActive(true);
                timerText.timerActive(true);
                timerText.setTimer(160f);
                timerText.setInstuctions("Practice feeding the bear!");
                break;
            case "Level9":
                timerText.gameObject.SetActive(true);
                timerText.timerActive(true);
                timerText.setTimer(160f);
                timerText.setInstuctions("Feed the bear for real!");
                timerText.setStartingPositions("Speedrun");
                break;
            case "Level7(BigxSmaller)":
                timerText.gameObject.SetActive(true);
                timerText.timerActive(true);
                timerText.setInstuctions("Keep the grimoire away from the others!");
                timerText.setTimer(62f);
                timerText.setGrimoire();
                dontcheckplayers = true;
                return;
            case "Credit":
                timerText.gameObject.SetActive(true);
                timerText.timerActive(false);
                timerText.setInstuctions("");
                timerText.setStartingPositions("Credit");
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
        if(pressJtimer > 0)
        {
            pressJtimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(SceneManager.sceneCount > 1 && pressJtimer < 0)
            { 
                SceneManager.UnloadSceneAsync("Level5(24x16) 2");
                GameObject.Find("UI Camera").GetComponent<AudioSource>().enabled = true;
                timmy.activatelevelKeeper(true);
                destroyApples();

                pressJtimer = 3f;
                


            }
            else if(pressJtimer <0)
            {
                // saveActiveScene();
                GameObject.Find("UI Camera").GetComponent<AudioSource>().enabled = false;
                timmy.setlevelKeeper(levelKeeper);
                //timmy.setchristmasmiracle(true);
                SceneManager.LoadScene("Level5(24x16) 2", LoadSceneMode.Additive);
                timmy.activatelevelKeeper(false);
                pressJtimer = 3f;

            }
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (timerText.getLastLevel() == "Level2")
                timerText.setLastLevel("Level3");
            if (timerText.getLastLevel() == "Level1")
                timerText.setLastLevel("Level2");
            if (timerText.getLastLevel()== "Nothing")
                timerText.setLastLevel("Level1");

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("Level7(BigxSmaller) 1");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if(SceneManager.GetActiveScene().name == "Level10(Super Hardcore)")
            {
                SceneManager.LoadScene("Hub(24x16)");
            }
            else
            {
                SceneManager.LoadScene("Level10(Super Hardcore)");
            }

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
                timerText.playerDied();
                livingPlayers = i;
            }
            if (i == 1 && GameObject.Find("UI Camera").GetComponent<GameManager>().connectedControllers !=1)
            {
                loadNextLevel();
            }
        }
    }
    public void loadNextLevel()
    {
        saveSimpleControls();
        players.Clear();
        dontcheckplayers = true;

        if (SceneManager.GetActiveScene().name == "Hub(24x16)" && timerText.getLastLevel() == "Nothing")
        {
            timerText.setLastLevel("Nothing");
            SceneManager.LoadScene("Level8");
        }
        if (SceneManager.GetActiveScene().name == "Level8")
        {
            timerText.setLastLevel("Level1");
            SceneManager.LoadScene("Hub(24x16)");
        }
        if (SceneManager.GetActiveScene().name == "Hub(24x16)" && timerText.getLastLevel() == "Level1")
        {
            timerText.setLastLevel("Level1");
            SceneManager.LoadScene("Level9");
        }
        if (SceneManager.GetActiveScene().name == "Level9")
        {
            timerText.setLastLevel("Level2");
            SceneManager.LoadScene("Hub(24x16)");
        }
        if (SceneManager.GetActiveScene().name == "Hub(24x16)" && timerText.getLastLevel() == "Level2")
        {
            timerText.setLastLevel("Level3");
            SceneManager.LoadScene("Level7(BigxSmaller)");
        }
        if (SceneManager.GetActiveScene().name== "Level7(BigxSmaller)")
        {
            SceneManager.LoadScene("Credit");
        }
        if (SceneManager.GetActiveScene().name == "Credit")
        {
            SceneManager.LoadScene("Hub(24x16)");
        }
        

        timerText.setInstuctions("");

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
    private void destroyApples()
    {
        GameObject[] apples = new GameObject[GameObject.FindGameObjectsWithTag("Rune").Length];
        apples = GameObject.FindGameObjectsWithTag("Rune");
        foreach (GameObject a in apples)
        {
            Destroy(a);
        }
    }
    private void saveActiveScene()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if(go.transform.parent=null )
            {
                go.transform.parent = levelKeeper.transform;
            }
        }
    }
}
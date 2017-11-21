using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance = null;
    private int level = 0;
    private string[] levelorder = { "CharacterControllerDevelopmentScene", "Level4(24x16) 1" };
    private GameObject[] players;

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
        if (i < 4)
        {
            print("you did die");
            loadNextLevel();
        }
    }
    void loadNextLevel()
    {
        level++;
        SceneManager.LoadScene(levelorder[level]);
    }
}
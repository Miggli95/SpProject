using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance = null;
    private int level = 0;
    private int[] levelorder;
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


    }

    public void startLevel()
    {

        //loadlevelstuff
        players = new GameObject[players.Length + GameObject.FindGameObjectsWithTag("Player").Length];
    }

    void Update()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int connectedControllers;
    GameObject[] players;
    List<string> activePlayers = new List<string>();
    CameraScript mainCamera;
   
	// Use this for initialization
	void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        mainCamera = Camera.main.GetComponent<CameraScript>();
        mainCamera.Init();
        UpdateConnectedPlayers();
        
    }

    void UpdateConnectedPlayers()
    {
        connectedControllers = ControllerKeyManager.ConnectedControllers();

        if (connectedControllers == 0)
            return;

        activePlayers.Clear();

        for (int i = 0; i < connectedControllers; i++)
        {
            int playerNumber = i + 1;
            string playerName = "P" + playerNumber;

            foreach (GameObject g in players)
            {
                if (g.name == playerName)
                    activePlayers.Add(playerName);
            }

        }

        foreach (GameObject player in players)
        {
            if (activePlayers.Contains(player.name))
            {
                player.SetActive(true);
                player.GetComponent<Controller2D>().InitControlls(true);
                player.GetComponent<Controller2D>().updateIgnoreCollisions();
                mainCamera.AddPlayer(player.name);
            }

            else
            {
                player.SetActive(false);
                mainCamera.RemovePlayer(player.name);
            }
        }
        
    }

	// Update is called once per frame
	void Update ()
    {
        if(connectedControllers != ControllerKeyManager.ConnectedControllers())
        {
            UpdateConnectedPlayers();
            GetComponent<Timer>().addPlayer(connectedControllers);
        }
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Hub(24x16)")
        {
            Debug.Log("Scene loaded");
            Awake();
        }
    }
}

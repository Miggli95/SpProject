using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameChecker : MonoBehaviour
{
    private BoxCollider boxy;
    private GameObject[] players;
    public GameObject levelManager;
    // Use this for initialization
    void Start()
    {
        boxy = GetComponent<BoxCollider>();
        players = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        players = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (GameObject p in players)
        {
            if (boxy.bounds.Contains(p.transform.position))
            {
                i++;
            }
        }
        if (i == players.Length)
        {
            levelManager.GetComponent<LevelManager>().loadNextLevel();
        }
    }
}

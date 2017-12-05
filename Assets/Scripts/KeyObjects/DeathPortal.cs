using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPortal : MonoBehaviour {

    private List<Controller2D> players;
    private Timer timmy;
    private float LifeTime;
    private float TimeAlive;
    private int score;
    private void Start()
    {
        timmy = GameObject.Find("UI Camera").GetComponent<Timer>();
    }
    // Update is called once per frame
    void Update ()
    {
        if (TimeAlive >= LifeTime)
        {
            switch(players.Count){
                case 1:
                    score = 40;
                    break;
                case 2:
                    score = 20;
                    break;
                case 3:
                    score = 10;
                    break;
                case 4:
                    score = 10;
                    break;
            }
            foreach (Controller2D p in players)
            {
                timmy.runeGet(p.name, score);
                p.doDeath();
            }
            Destroy(this.gameObject);
        }
        else
        {
            TimeAlive += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Controller2D>();
            players.Add(player);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Controller2D>();
            if (players.Contains(player))
            {
                players.Remove(player);
            }
        }
    }

   

    public void Spawn(float LifeTime)
    {
        players = new List<Controller2D>();
        this.LifeTime = LifeTime;
        TimeAlive = 0;
    }
}

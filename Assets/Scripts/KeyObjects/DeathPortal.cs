﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPortal : MonoBehaviour {

    private List<Controller2D> players;
    private Timer timmy;
    private float LifeTime = 3f;
    private float TimeAlive;
    private int score;
    private bool doOnce = true;
    private bool timetoclear = false;
    private void Start()
    {
        timmy = GameObject.Find("UI Camera").GetComponent<Timer>();
    }
    // Update is called once per frame
    void Update ()
    {
        killPlayers();
        timetoclear = true;
        if (TimeAlive >= LifeTime)
        {
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
    private void killPlayers()
    {
        if (doOnce)
        {
            switch (players.Count)
            {
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



        }
        if (timetoclear)
        {
            players.Clear();
            this.GetComponent<BoxCollider>().enabled = false;
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
        doOnce = true;
        players = new List<Controller2D>();
        TimeAlive = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour {

    private List<Controller2D> players;
    private float LifeTime;
    private float TimeAlive;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Controller2D>();
            players.Add(player);
            player.setSlowed(true);
            player.speed = 2;

            //slow down player speed
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
                player.setSlowed(false);
                player.speed = 5;
                //return player speed to normal
            }

        }
    }

    void Update()
    {
        if(TimeAlive >= LifeTime)
        {

            foreach(Controller2D p in players)
            {
                p.speed = 5;
            }
            Destroy(this.gameObject);
            //reset players speed
            //remove all players from list
            //remove this object
        }
        else
        {
            TimeAlive += Time.deltaTime;
        }
    }

    public void Spawn(float LifeTime)
    {
        players = new List<Controller2D>();
        this.LifeTime = LifeTime;
        TimeAlive = 0;
    }
    private void OnDestroy()
    {
        foreach (Controller2D p in players){
            p.setSlowed(false);
        }
    }




}

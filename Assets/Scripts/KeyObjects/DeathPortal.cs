using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPortal : MonoBehaviour {

    private List<Controller2D> players;
    private float LifeTime;
    private float TimeAlive;

	
	// Update is called once per frame
	void Update ()
    {
        if (TimeAlive >= LifeTime)
        {
            foreach (Controller2D p in players)
            {
                p.doDeath();
            }
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

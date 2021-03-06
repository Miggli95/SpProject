﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonScript : MonoBehaviour {
    private BoxCollider boxy;
    private List<Controller2D> players;
    private float countdown = 0f;
    private bool start = false;
    public GameObject roof;
    // Use this for initialization
    void Start () {
        boxy = GetComponent<BoxCollider>();
        players = new List<Controller2D>();

    }
	
	// Update is called once per frame
	void Update () {
		if(players.Count == 0)
        {
            countdown = 0f;
        }
        if(countdown > 0f)
        {
            countdown -= Time.deltaTime;
            if(countdown < 0f)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1.5f, this.transform.position.z);
                cube.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y-3, this.transform.localScale.z);
                countdown = 0f;
                if(start)
                {
                    roof.SetActive(false);
                }
            }
        }
        
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(CharacterController) && !other.GetComponent<Controller2D>().isGhost)
        {
            if (players.Count == 0)
            {
                switch (this.name)
                {
                    case "1st":
                        countdown = 2.2f;
                        start = true;
                        break;
                    case "2nd":
                        countdown = 2.3f;
                        start = true;
                        break;
                    case "3rd":
                        countdown = 2.4f;
                        start = true;
                        break;
                    case "4th":
                        countdown = 2.5f;
                        start = true;
                        break;
                    default:
                        countdown = 2f;
                        break;
                   

                }
            }
            var player = other.GetComponent<Controller2D>();
            players.Add(player);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetType() == typeof(CharacterController) && !other.GetComponent<Controller2D>().isGhost)
        {
            var player = other.GetComponent<Controller2D>();
            if (players.Contains(player))
            {
                players.Remove(player);
            }
           

        }
    }
}

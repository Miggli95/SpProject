﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonB : MonoBehaviour
{
    private BoxCollider boxy;
    private List<Controller2D> players;
    private float countdown = 0f;
    // Use this for initialization
    void Start()
    {
        boxy = GetComponent<BoxCollider>();
        players = new List<Controller2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count == 0)
        {
            countdown = 0f;
        }
        if (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            if (countdown < 0f)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(this.transform.position.x , this.transform.position.y - 2f, this.transform.position.z);
                cube.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - 2, this.transform.localScale.z);
                countdown = 0f;
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
                        countdown = 2f;
                        break;
                    case "2nd":
                        countdown = 3f;
                        break;
                    case "3rd":
                        countdown = 4f;
                        break;
                    case "4th":
                        countdown = 5f;
                        break;
                    default:
                        countdown = 4f;
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

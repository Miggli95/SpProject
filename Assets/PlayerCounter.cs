using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCounter : MonoBehaviour {

    // Use this for initialization

    public int numberOfPlayers;

    public TextMesh text;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Start game" + "\n" + "Players:" + numberOfPlayers + "/4";


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "P1")
        {
            numberOfPlayers++;

        }

        if (other.gameObject.name == "P2")
        {
            numberOfPlayers++;

        }

        if (other.gameObject.name == "P3")
        {
            numberOfPlayers++;

        }


        if (other.gameObject.name == "P4")
        {
            numberOfPlayers++;

        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "P1")
        {
            numberOfPlayers--;

        }

        if (other.gameObject.name == "P2")
        {
            numberOfPlayers--;

        }

        if (other.gameObject.name == "P3")
        {
            numberOfPlayers--;

        }


        if (other.gameObject.name == "P4")
        {
            numberOfPlayers--;

        }
    }
}

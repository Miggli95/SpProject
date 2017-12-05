using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPositions : MonoBehaviour {

    public List<GameObject> startingPositions;

    public void setPositions(int[] playerID)
    {
        for(int i = 0; i < playerID.Length; i++)
        {
            switch (playerID[i])
            {
                case 1:
                    GameObject.Find("P1").transform.position = startingPositions[i].transform.position;
                    break;
                case 2:
                    GameObject.Find("P2").transform.position = startingPositions[i].transform.position;
                    break;
                case 3:
                    GameObject.Find("P3").transform.position = startingPositions[i].transform.position;
                    break;
                case 4:
                    GameObject.Find("P4").transform.position = startingPositions[i].transform.position;
                    break;
            }

        }
    }
}

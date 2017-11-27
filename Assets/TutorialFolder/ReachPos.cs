using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ReachPos : MonoBehaviour
{

    public int id;
    //public Objective objective;
	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
		



	}

  void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<CheckList>().getObjective(id).done)
            {
                other.GetComponent<CheckList>().getObjective(id).done = true;
            }
        }
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {

        gameObject.transform.GetChild(0).gameObject.SetActive(false);

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag ("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}

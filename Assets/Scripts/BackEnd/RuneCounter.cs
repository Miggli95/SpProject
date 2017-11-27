using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RuneCounter : MonoBehaviour {
    public Timer score;

    // Use this for initialization
    void Start () {
        score = GameObject.Find("UI Camera").GetComponent<Timer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetType() == typeof(CharacterController))
        {

            score.runeGet(other.gameObject.name);
            other.gameObject.GetComponent<Controller2D>().sizeUp();
            
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}

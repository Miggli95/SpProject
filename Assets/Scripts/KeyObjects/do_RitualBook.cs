using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class do_RitualBook : PickUpKeyObject {

    private float Timer;

	// Use this for initialization
	void Start () {
        base.Initialize("RitualBook");
        Timer = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().timerText.GetComponent<Timer>().getTimer();
	}
	
	// Update is called once per frame
	void Update () {
		if(Timer <= 0)
        {
            //create death portal
        }
        else
        {
            Timer -= Time.deltaTime;
        }
	}

    public override bool Use(Controller2D player)
    {
        return false;
    }
}

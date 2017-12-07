using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class do_RitualBook : PickUpKeyObject {

    private float Timer;
    public GameObject portal;

	// Use this for initialization
	void Start () {
        base.Initialize("RitualBook");
        Timer = 58f;
        //TODO add so that this object can know what the level timer is.
	}
	
	// Update is called once per frame
	void Update () {
		if(Timer <= 0)
        {
            var obj = Instantiate(portal, this.transform.position, this.transform.rotation);
            obj.GetComponent<DeathPortal>().Spawn(0.25f);
            Destroy(this.gameObject);
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

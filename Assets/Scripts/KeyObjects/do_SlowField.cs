using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;

public class do_SlowField : pickUpDisruptiveObject {


    
    public float Timer;
    public float Countdown;
    public GameObject SlowZone;

    public override bool Use(Controller2D player)
    {
        Debug.Log("Used slowfield");
        state = pickUpState.Used;
        Timer = Countdown;
        return false;
    }

    // Use this for initialization
    void Start () {
        base.Initialize();
	}

    void Update()
    {
        if (state != pickUpState.Used)
            return;
        if(Timer <= 0)
        {
            // create slowfield prefab
            var obj = Instantiate(SlowZone, this.transform.position, this.transform.rotation);
            obj.GetComponent<SlowZone>().Spawn(5f);
            Destroy(this.gameObject);
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public class do_SpiderBomb : pickUpDisruptiveObject {

    private Controller2D player;
    public float timer;
    public float fuse;
    public float throwX;
    public float throwY;
    private bool fuseLit;
    public GameObject SlowZone;
	
	void Start () {
        base.Initialize();
        fuseLit = false;
	}
	
	// Update is called once per frame
	public void Update () {
        if (!fuseLit)
            return;
		if(timer <= 0)
        {
            if (state == pickUpState.PickedUp)
                player.forceDrop();
            var obj = Instantiate(SlowZone, this.transform.position, this.transform.rotation);
            obj.GetComponent<SlowZone>().Spawn(5f);
            Destroy(this.gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }


	}

    public override bool Use(Controller2D player)
    {
        state = pickUpState.Used;
        var ren = player.GetComponent<SpriteRenderer>();
        int dir = ren.flipX ? 1 : -1;
        KnockAway(new Vector3(throwX * dir, throwY));
        return false;
    }

    public override IPickUp PickUp(Controller2D player)
    {
        if(base.PickUp() == null)
        {
            return null;
        }
        this.player = player;
        fuseLit = true;
        timer = fuse;
        Debug.Log(fuseLit);
        return this;
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(state != pickUpState.PickedUp && fuseLit && other.CompareTag("Player"))
        {
            var obj = Instantiate(SlowZone, this.transform.position, this.transform.rotation);
            obj.GetComponent<SlowZone>().Spawn(5f);
            Destroy(this.gameObject);
        }
    }

    public override void Drop()
    {
        if (fuseLit)
        {
            state = pickUpState.Used;
        } else
        {
            state = pickUpState.Waiting;
        }
    }
}

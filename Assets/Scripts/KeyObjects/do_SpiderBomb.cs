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
	}
	
	// Update is called once per frame
	void Update () {
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
        var ren = player.GetComponent<SpriteRenderer>();
        int dir = ren.flipX ? 1 : -1;
        KnockAway(new Vector3(dir * throwX, throwY));
        return false;
    }

    public override IPickUp PickUp(Controller2D player)
    {
        if(base.PickUp() == null)
        {
            return null;
        }
        this.player = player;
        timer = fuse;
        return this;
    }
}

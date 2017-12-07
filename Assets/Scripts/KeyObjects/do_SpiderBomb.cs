using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public class do_SpiderBomb : pickUpDisruptiveObject {

    private Controller2D player;
    private List<Controller2D> players;
    private SoundManagerScript soundy;
    public float timer;
    public float fuse;
    public float throwX;
    public float throwY;
    private bool fuseLit;
    private float gracePeriod;
    private float graceTimer;
    private bool grace;
    private Material og;
    private SpriteRenderer rend;
    //public Material red;
    public GameObject SlowZone;
	
	void Start () {
        base.Initialize();
        fuseLit = false;
        players = new List<Controller2D>();
        rend = GetComponent<SpriteRenderer>();
        gracePeriod = 0.3f;
        graceTimer = 0f;
        grace = false;
        og = rend.material;
        soundy = GameObject.Find("UI Camera").GetComponent<SoundManagerScript>();
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
            clearFocus();
            soundy.PlaySound("explode");
            Destroy(this.gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
       /* if(timer <2.5 && timer > 2.4)
        {
            rend.material = red;
        }
        if (timer < 2.1 && timer > 2.0)
        {
            rend.material = og;
        }
        if (timer < 1.8 && timer > 1.7)
        {
            rend.material = red;
        }
        if (timer < 1.5 && timer > 1.4)
        {
            rend.material = og;
        }
        if (timer < 1.3 && timer > 1.2)
        {
            rend.material = red;
        }
        if (timer < 1.0 && timer > 0.9)
        {
            rend.material = og;
        }
        if (timer < 0.9 && timer > 0.8)
        {
            rend.material = red;
        }
        if (timer < 0.8 && timer > 0.7)
        {
            rend.material = og;
        }
        if (timer < 0.7 && timer > 0.6)
        {
            rend.material = red;
        }
        if (timer < 0.6 && timer > 0.5)
        {
            rend.material = og;
        }
        if (timer < 0.5 && timer > 0.4)
        {
            rend.material = red;
        }
        if (timer < 0.4 && timer > 0.3)
        {
            rend.material = og;
        }
        if (timer < 0.3 && timer > 0.2)
        {
            rend.material = red;
        }
        if (timer < 0.2 && timer > 0.1)
        {
            rend.material = og;
        }
        */


        if (!grace)
            return;
        if(graceTimer >= gracePeriod)
        {
            grace = false;
        }
        else
        {
            graceTimer += Time.deltaTime;
        }


	}

    private void clearFocus()
    {
        foreach (Controller2D p in players)
            p.removePickUpFocus(this);
    }

    public override bool Use(Controller2D player)
    {
        if (player.charInput.y < 0)
        {
            player.forceDrop();
            state = pickUpState.Used;
        }
        else
        {
            grace = true;
            state = pickUpState.Used;
            var ren = player.GetComponent<SpriteRenderer>();
            int dir = ren.flipX ? 1 : -1;
            KnockAway(new Vector3(throwX * dir, throwY));
        }
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
        if(state != pickUpState.PickedUp && fuseLit && other.CompareTag("Player"))
        {
            if (grace && other.GetComponent<Controller2D>() == player)
                return;
            var obj = Instantiate(SlowZone, this.transform.position, this.transform.rotation);
            obj.GetComponent<SlowZone>().Spawn(5f);
            Destroy(this.gameObject);
        } else if(other.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);
            players.Add(other.GetComponent<Controller2D>());
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
            players.Remove(other.GetComponent<Controller2D>());
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

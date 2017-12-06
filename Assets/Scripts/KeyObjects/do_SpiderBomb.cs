﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public class do_SpiderBomb : pickUpDisruptiveObject {

    private Controller2D player;
    private List<Controller2D> players;
    public float timer;
    public float fuse;
    public float throwX;
    public float throwY;
    private bool fuseLit;
    private float gracePeriod;
    private float graceTimer;
    private bool grace;
    private Shader og;
    public Shader red;
    public GameObject SlowZone;
	
	void Start () {
        base.Initialize();
        fuseLit = false;
        players = new List<Controller2D>();
        gracePeriod = 0.3f;
        graceTimer = 0f;
        grace = false;
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
            Destroy(this.gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 2)
        {

        }

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

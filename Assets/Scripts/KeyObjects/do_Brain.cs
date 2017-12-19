using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public class do_Brain : pickUpDisruptiveObject {

    private Controller2D usingPlayer;
    private Vector3 throwDir;
    public Sprite brainActive;
    private SpriteRenderer ren;
    public float throwX;
    public float throwY;
    private bool hitSomething;
    private bool grace;
    private float gracePeriod;
    private float graceTimer;
    private Rigidbody rb;

    public void Start()
    {
        base.Initialize();
        graceTimer = 0f;
        gracePeriod = 0.7f;
        grace = false;
        ren = GetComponent<SpriteRenderer>();
        ID = "Brain";
    }

    public override void OnTriggerEnter(Collider other)

    {
        if(state == pickUpState.Waiting)
            base.OnTriggerEnter(other);

        if (state == pickUpState.Used && other.CompareTag("Player"))
        {
            var HitPlayer = other.GetComponent<Controller2D>();
            if (grace && HitPlayer == usingPlayer)
                return;
            HitPlayer.stopMove(1f);
            HitPlayer.forceDrop();
            Destroy(this.gameObject);
        }
        
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(state == pickUpState.Used)
            ren.sprite = brainActive;
    }

    public override bool Use(Controller2D player)
    {
        playSound("throw");
        usingPlayer = player;
        if (player.charInput.y < -0.1)
        {
            player.forceDrop();
            state = pickUpState.Used;
        } else if(player.charInput.y > .80)
        {
            state = pickUpState.Used;
            KnockAway(new Vector3(0, throwY * 2));
            grace = true;
        }
        else
        {
            state = pickUpState.Used;
            var ren = player.GetComponent<SpriteRenderer>();
            int dir = ren.flipX ? 1 : -1;
            KnockAway(new Vector3(throwX * dir, throwY));
            grace = true;
        }
        return false;
    }

    public void Update()
    {
        if (!grace)
            return;
        if (graceTimer >= gracePeriod)
        {
            grace = false;
        }
        else
        {
            graceTimer += Time.deltaTime;
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public class do_Brain : pickUpDisruptiveObject {

    private Controller2D usingPlayer;
    private Vector3 throwDir;
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

    public override bool Use(Controller2D player)
    {
        usingPlayer = player;
        if (player.charInput.y < 0)
        {
            player.forceDrop();
            state = pickUpState.Used;
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

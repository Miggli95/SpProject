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
    private Rigidbody rb;

    public void Start()
    {
        base.Initialize();
        
    }

    public override void OnTriggerEnter(Collider other)

    {
        if(state == pickUpState.Waiting)
            base.OnTriggerEnter(other);

        if (state == pickUpState.Used && other.CompareTag("Player"))
        {
            var HitPlayer = other.GetComponent<Controller2D>();
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
        state = pickUpState.Used;
        var ren = player.GetComponent<SpriteRenderer>();
        int dir = ren.flipX ? 1 : -1;
        KnockAway(new Vector3(throwX * dir, throwY));

        return false;
    }

    

}

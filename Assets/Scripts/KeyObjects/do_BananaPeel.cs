using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public class do_BananaPeel : pickUpDisruptiveObject {

    private Controller2D usingPlayer;
    private Vector3 throwDir;

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player") && state == pickUpState.Used)
        {
            var HitPlayer = other.GetComponent<Controller2D>();
            if(usingPlayer != HitPlayer)
            {
                HitPlayer.stopMove(1f);
                HitPlayer.forceDrop();
            }
        }
        
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    public override bool Use(Controller2D player)
    {
        state = pickUpState.Used;
        throwDir = new Vector3(player.moveDir.x, 0);
        return false;
    }

    public void Update()
    {
        if (state != pickUpState.Used)
            return;
        transform.Translate(throwDir * Time.deltaTime);
    }

}

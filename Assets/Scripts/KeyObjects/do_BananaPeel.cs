using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public class do_BananaPeel : pickUpDisruptiveObject {

    private Controller2D usingPlayer;
    private Vector3 throwDir;
    public float throwSpeed;
    public float fallSpeed;
    private bool hitSomething;
    private Rigidbody rb;

    public void Start()
    {
        base.Initialize();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

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
        player.forceDrop();
        hitSomething = false;
        var direction = player.moveDir.x > 0 ? throwSpeed : -throwSpeed;
        Debug.Log(direction);
        throwDir = new Vector3(direction, 5);
        rb.AddForce(throwDir);
        return false;
    }

    public void Update()
    {
        /*
        if (state != pickUpState.Used || hitSomething)
            return;
        transform.Translate(throwDir * Time.deltaTime);
        reduceSpeed();
        RaycastHit hit;
        Physics.Raycast(transform.position, throwDir * Time.deltaTime, out hit);
        */

    }

    public void reduceSpeed()
    {
        /*
        throwDir.x -= (throwDir.x * 0.005f);
        throwDir.y += -fallSpeed;
        fallSpeed *= 1.05f;
        */
    }

}

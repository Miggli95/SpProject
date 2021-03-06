﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
using System;

public abstract class pickUpDisruptiveObject : MonoBehaviour, IPickUp
{
    public pickUpState state;
    protected ObjectGravity ObjGrav;
    protected string ID;
    public virtual void Drop()
    {
        state = pickUpState.Waiting;
        //Need to implement so that a object falls to the ground. 
    }

    public virtual void Initialize()
    {
        state = pickUpState.Waiting;
        ObjGrav = this.GetComponent<ObjectGravity>();
        ID = "Disruptive";
    }

    public virtual string getID()
    {
        return ID;
    }

    public void Outline()
    {
        
    }

    public virtual IPickUp PickUp()
    {
        if(state != pickUpState.Waiting)
        {
            return null;
        }
        state = pickUpState.PickedUp;
        return this;
    }

    public void removeOutline()
    {
        
    }

    public abstract bool Use(Controller2D player);

    public virtual void respawn(Vector2 v)
    {
        transform.position = v;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Controller2D>().addPickUpFocus(this);
            //Set pickup focus in player script to this
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            other.GetComponent<Controller2D>().removePickUpFocus(this);
            //remove pickup focus in controller2d
        }
    }

    public void updatePos(Vector3 pos)
    {
        this.transform.position = pos;
    }
    public virtual void Consume()
    {
        Destroy(this);
    }

    public void KnockAway(Vector3 dir)
    {
        ObjGrav.knockedAway(dir);
    }

    public virtual IPickUp PickUp(Controller2D player)
    {
        if (state != pickUpState.Waiting)
        {
            return null;
        }
        state = pickUpState.PickedUp;
        return this;
    }

    protected void playSound(string s)
    {
        SoundManagerScript.PlaySound(s);
    }

    public virtual void StartTimer(){ }

    public virtual void StopTimer() { }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
//[RequireComponent(typeof(Collider2D))]
public abstract class PickUpKeyObject : KeyObject, IPickUp
{
    public string message;
    //private player pickUpPlayer
    public pickUpState state;


    public void Drop() {                            //Implement functionallity to keep the object from overlapping with other IPickUp objects and make it fall to the ground. Perhaps in update?
        state = pickUpState.Waiting;

    }

    public virtual IPickUp PickUp(){
        if (state != pickUpState.Waiting)           //Plan is to let player keep track of the pickup's position and update it. Should pickup know anything about the player that picked it up?
        {                                            //Should a player be able to pick up used objects? Should used objects disappear? Should some disappear?
            return null;                             //Exists to allow items do special things when they're picked up, instead of setting the focus to pick up in controller2d
        }
        Debug.Log("Picked up");
        state = pickUpState.PickedUp;               //Returns this object if it's not in the picked up state.
        return this;    

    }

    public virtual bool Use()
    {
        return true;                                //virtual method, each object has/needs it's own unique use function, returns true if the use is called on a keyobject with interface IPickUp
    }
    public new void innitialize(string id) {
        base.innitialize(id);
        state = pickUpState.Waiting;
    }

    public void OnTriggerEnter(Collider other) //need to change to 2D collider in the future
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enter");
            other.GetComponent<Controller2D>().addPickUpFocus(this);
            //Set pickup focus in player script to this
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("exit");
            other.GetComponent<Controller2D>().removePickUpFocus(this);
            //remove pickup focus in player script if the object that is in focus is this object.
        }
    }

    public string getID()
    {
        return id;
    }

    public void Outline() //Needs code to represent that an object is highlighted, need to take a value
    {
        Debug.Log("enter:" + message);
    }

    public void removeOutline()
    {
        Debug.Log("exit: " + message);
    }

    public void updatePos(Vector3 pos)
    {
        this.transform.position = pos;
    }
}

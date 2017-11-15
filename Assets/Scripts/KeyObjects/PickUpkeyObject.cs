﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
[RequireComponent(typeof(Collider2D))]
public abstract class PickUpKeyObject : KeyObject, IPickUp
{

    //private player pickUpPlayer
    public pickUpState state = pickUpState.Waiting;


    public void Drop() {                            //Implement functionallity to keep the object from overlapping with other IPickUp objects and make it fall to the ground. Perhaps in update?
        state = pickUpState.Waiting;

    }

    public virtual IPickUp PickUp(){
        if (state == pickUpState.PickedUp)           //Plan is to let player keep track of the pickup's position and update it. Should pickup know anything about the player that picked it up?
        {                                            //Should a player be able to pick up used objects? Should used objects disappear? Should some disappear?
            return null;
        }
        state = pickUpState.PickedUp;               //Returns this object as a IPickUp if it's not in the picked up state.
        return this;    

    }

    public abstract bool Use(); //abstract method, each object has/needs it's own unique use function, returns false if the use is called on a keyobject with interface IPickUp

    public new void innitialize(string id) {
        base.innitialize(id);
        state = pickUpState.Waiting;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            //Set pickup focus in player script to this
        }
    }
    public void OnTriggerExit2d(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            //remove pickup focus in player script if the object that is in focus is this object.
        }
    }

    public string getID()
    {
        return id;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public abstract class PickUpKeyObject : KeyObject, IPickUp
{

    //private player pickUpPlayer
    public pickUpState state = pickUpState.Waiting;


    public void Drop() {
        state = pickUpState.Waiting;

    }

    public void PickUp(){
        state = pickUpState.PickedUp;

    }

    public abstract void Use();

    public PickUpKeyObject(string id) : base(id) {}
}

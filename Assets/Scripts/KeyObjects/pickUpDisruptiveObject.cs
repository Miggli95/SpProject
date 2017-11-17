using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
using System;

public abstract class pickUpDisruptiveObject : MonoBehaviour, IPickUp
{
    public pickUpState state;

    public void Drop()
    {
        state = pickUpState.Waiting;
        //Need to implement so that a object falls to the ground. 
    }

    public virtual void innitialize()
    {
        state = pickUpState.Waiting;
    }

    public virtual string getID()
    {
        return "disruptive";
    }

    public void Outline()
    {
        throw new NotImplementedException();
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

    public abstract bool Use();

    public virtual void respawn(Vector2 v)
    {
        transform.position = v;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            other.GetComponent<Controller2D>().addPickUpFocus(this);
            //Set pickup focus in player script to this
        }
    }
    public void OnTriggerExit2d(Collider2D other)
    {
        if (other.CompareTag("player"))
        {

            other.GetComponent<Controller2D>().removePickUpFocus(this);
            //remove pickup focus in controller2d
        }
    }
}

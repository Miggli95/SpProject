using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
//[RequireComponent(typeof(Collider2D))]
public abstract class PickUpKeyObject : KeyObject, IPickUp
{

    //private player pickUpPlayer
    public pickUpState state = pickUpState.Waiting;
    private ObjectGravity ObjGrav;

    public void Drop() {                            //Implement functionallity to keep the object from overlapping with other IPickUp objects and make it fall to the ground. Perhaps in update?
        state = pickUpState.Waiting;

    }

    public virtual IPickUp PickUp(){
        if (state != pickUpState.Waiting)           //Plan is to let player keep track of the pickup's position and update it. Should pickup know anything about the player that picked it up?
        {                                            //Should a player be able to pick up used objects? Should used objects disappear? Should some disappear?
            return null;                             //Exists to allow items do special things when they're picked up, instead of setting the focus to pick up in controller2d
        }
        state = pickUpState.PickedUp;               //Returns this object as a IPickUp if it's not in the picked up state.
        return this;    

    }

    public virtual bool Use(Controller2D player)
    {
        return true;                                //abstract method, each object has/needs it's own unique use function, returns false if the use is called on a keyobject with interface IPickUp
    }
    public new void Initialize(string id) {
        base.Initialize(id);
        state = pickUpState.Waiting;
        ObjGrav = this.GetComponent<ObjectGravity>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Enter");
            other.GetComponent<Controller2D>().addPickUpFocus(this);
            //Set pickup focus in player script to this
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Exit");
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

    }

    public void removeOutline()
    {

    }
    public void updatePos(Vector3 pos)
    {
        this.transform.position = pos;
    }
    public virtual void Consume()
    {
        state = pickUpState.Used;
    }
    public void KnockAway(Vector3 dir)
    {
        ObjGrav.knockedAway(dir);
    }

    public IPickUp PickUp(Controller2D player)
    {
        if (state != pickUpState.Waiting)
        {
            return null;
        }
        state = pickUpState.PickedUp;
        return this;
    }
}

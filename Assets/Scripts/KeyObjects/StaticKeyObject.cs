using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;

public abstract class StaticKeyObject : KeyObject, IInteractable
{
    //add in field that keeps track of the player that completed the goal.
    protected string requirement; //Refers to another keyobject, if requirement isn't null check what the id of the object the player is carrying is. 
    //(Let all objects have an id? Use a disruptive object as a key object?)
    private bool isComplete;

    private InteractableState state; // Check what state it is in to decide if methods should run their course.
    public void Interact() // Need to call base in classes derived from StaticKeyObject in general. Checks what state this interactable is in. Need to add argument player to keep track of who
                           // interacted with the object if the object should sacrifice the player.
    {
        if(state != InteractableState.Enabled)
        {
            return;
        }
        state = InteractableState.Interacted;
    }
    protected void flagComplete()
    {
        isComplete = true;
    }

    public void Enable()
    {
        state = InteractableState.Enabled;
    }

    protected void innitialize(string id, string requirement) //innitialize method that presumes that the object should start as dormant
    {
        base.innitialize(id);
        this.requirement = requirement;
        state = InteractableState.Dormant;
        
    }

    protected void innitialize(string id, string requirement, InteractableState state) //innitialize method that lets you decide what state it starts in
    {
        base.innitialize(id);
        this.requirement = requirement;
        this.state = state;
    }

    public bool getisComplete()
    {
        return isComplete;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //Set what interactable the player is currently near, try to see if you can get information about what object the player is carrying(keyobject id), save player temporary to be able to kill him?
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
            //Reset what the interactable the player is currently near, remove what information we got about the player and object the player is carrying(keyobject id)
    }


}

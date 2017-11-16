using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
[RequireComponent(typeof(Collider2D))]
public abstract class StaticKeyObject : KeyObject, IInteractable
{
    //add in field that keeps track of the player that completed the goal? Or let level goal keep track of who completed the earlier stages?.
    protected List<string> requirement; //Refers to another keyobject, if requirement isn't null check what the id of the object the player is carrying is. use an array instead? 
    //(Let all objects have an id? Use a disruptive object as a key object?)
    private bool isComplete = false;
    protected Controller2D interactingPlayer = null;

    protected InteractableState state; // Check what state it is in to decide if methods should run their course.

    public virtual bool Interact(Controller2D player) // Need to call base in classes derived from StaticKeyObject in general. Checks what state this interactable is in. 
                           // interacted with the object if the object should sacrifice the player.
    {
        if(state != InteractableState.Enabled)
        {
            return false;
        }
        if(interactingPlayer != null)
        {
            return false;
        }
        state = InteractableState.Interacted;
        interactingPlayer = player;
        if (!checkRequirement(player.getCarryId()))
        {
            return false;
        }
        return true;
    }
    protected void flagComplete()
    {
        isComplete = true;
    }

    public void Enable()
    {
        state = InteractableState.Enabled;
    }

    protected virtual void innitialize(string id, List<string> requirement) //innitialize method that presumes that the object should start as dormant
    {
        base.innitialize(id);
        this.requirement = requirement;
        state = InteractableState.Dormant;
        
    }

    protected virtual void innitialize(string id, List<string> requirement, InteractableState state) //innitialize method that lets you decide what state it starts in
    {
        base.innitialize(id);
        this.requirement = requirement;
        this.state = state;
    }
    protected virtual void innitialize(string id, InteractableState state)
    {
        base.innitialize(id);
        this.requirement = null;
        this.state = state;
    }
    protected virtual new void innitialize(string id)
    {
        base.innitialize(id);
        this.requirement = null;
        this.state = InteractableState.Dormant;
    }
    public bool getisComplete()
    {
        return isComplete;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            //Set what interactable the player is currently near, try to see if you can get information about what object the player is carrying(keyobject id), save player temporary to be able to kill him?
            other.GetComponent<Controller2D>().setInteractableFocus(this);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            //Reset what the interactable the player is currently near, remove what information we got about the player and object the player is carrying(keyobject id)
            other.GetComponent<Controller2D>().setInteractableFocus(null);
        }
    }

    public void sacrificeInteractingPlayer(int score)
    {
        //call sacrifice on interacting player.
    }

    private bool checkRequirement(string id)
    {
        if(requirement == null)
        {
            return true;
        }
        foreach(string s in requirement)
        {
            if(s == id)
            {
                removeRequirement(id);
                return true;
            }
        }
        return false;
    }

    private void removeRequirement(string id)
    {
        requirement.Remove(id);
        requirement.TrimExcess();
        
    }

    public Controller2D getInteractingPlayer()
    {
        return interactingPlayer;
    }


}

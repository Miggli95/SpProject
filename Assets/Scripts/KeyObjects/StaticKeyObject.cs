using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
//[RequireComponent(typeof(Collider2D))]
public abstract class StaticKeyObject : KeyObject, IInteractable
{
                                                    //add in field that keeps track of the player that completed the goal? Or let level goal keep track of who completed the earlier stages?.
    protected List<string> requirement;             //Refers to another keyobject, if requirement isn't null check what the id of the object the player is carrying is. use an array instead? 
                                                    //(Let all objects have an id? Use a disruptive object as a key object?)
    private bool isComplete = false;
    protected Controller2D interactingPlayer = null;
    protected InteractableState state;              // Check what state it is in to decide if methods should run their course.

    public virtual bool Interact(Controller2D player) // Need to call base in classes derived from StaticKeyObject in general. Checks what state this interactable is in. 
                           // interacted with the object if the object should sacrifice the player.
    {
        Debug.Log("Interact, super");
        if(state != InteractableState.Enabled)
        {
            Debug.Log("Wrong state");
            return false;
        }
        if(interactingPlayer != null)
        {
            Debug.Log("already an interacting player");
            return false;
        }
        state = InteractableState.Interacted;
        interactingPlayer = player;
        if (!checkRequirement(interactingPlayer.getCarryId()))
        {
            return false;
        }
        return true;
    }
    protected void flagComplete()
    {
        Debug.Log(id + " is complete!");
        isComplete = true;
    }

    public void Enable()
    {
        state = InteractableState.Enabled;
    }

    protected virtual void Initialize(string id, List<string> requirement) //innitialize method that presumes that the object should start as dormant
    {
        base.Initialize(id);
        this.requirement = requirement;
        state = InteractableState.Dormant;
        
    }

    protected virtual void Initialize(string id, List<string> requirement, InteractableState state) //innitialize method that lets you decide what state it starts in
    {
        base.Initialize(id);
        this.requirement = requirement;
        this.state = state;
    }
    protected virtual void Initialize(string id, InteractableState state)
    {
        base.Initialize(id);
        this.requirement = null;
        this.state = state;
    }
    protected virtual new void Initialize(string id)
    {
        base.Initialize(id);
        this.requirement = null;
        this.state = InteractableState.Dormant;
    }
    public bool getisComplete()
    {
        return isComplete;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player entered " + id);
            //Set what interactable the player is currently near, try to see if you can get information about what object the player is carrying(keyobject id), save player temporary to be able to kill him?
            other.GetComponent<Controller2D>().setInteractableFocus(this);

            if (checkPickup(other.GetComponent<Controller2D>().getCarryId()))
            {
                other.GetComponent<Controller2D>().canInteract = true;
            }

            else
            {
                other.GetComponent<Controller2D>().canInteract = false;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited " + id);
            other.GetComponent<Controller2D>().canInteract = false;
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
            Debug.Log("No requirement");
            return true;
        }
        foreach(string s in requirement)
        {
            if(s == id)
            {
                Debug.Log(s + " removed");
                interactingPlayer.consumeCarry(id);
                removeRequirement(id);
                return true;
            }
        }
        Debug.Log(id + "Wasn't found");
        return false;
    }

    private bool checkPickup(string id)
    {
        if (requirement == null)
        {
            Debug.Log("No requirement");
            return true;
        }
        foreach (string s in requirement)
        {
            if (s == id)
            {
                return true;
            }
        }
        Debug.Log(id + "Wasn't found");
        return false;
    }


    private void removeRequirement(string id)
    {
        requirement.Remove(id);
        requirement.TrimExcess();
        foreach (string s in requirement)
            Debug.Log(s);
        
    }

    public Controller2D getInteractingPlayer()
    {
        return interactingPlayer;
    }


}

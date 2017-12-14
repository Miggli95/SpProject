using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
using System;
//[RequireComponent(typeof(Collider2D))]
public abstract class staticDisruptiveObject : MonoBehaviour , IInteractable
{

    private bool isOneUse;
    protected InteractableState state;

    public void Enable()
    {
        state = InteractableState.Enabled;
    }


    public virtual bool Interact(Controller2D player)
    {
        if(state == InteractableState.Dormant)
        {
            return false;
        }
        if(!isOneUse && state != InteractableState.Enabled)
        {
            return false;
        }
        return true;
    }

    public void Initialize(bool isOneUse, InteractableState state)
    {
        this.isOneUse = isOneUse;
        this.state = state;
    }

    public void Initialize()
    {
        isOneUse = false;
        this.state = InteractableState.Enabled;
    }
    public void Initialize(bool isOneUse)
    {
        this.isOneUse = isOneUse;
        state = InteractableState.Enabled;
    }
    public void Initialize(InteractableState state)
    {
        this.state = state;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name);
        if (other.CompareTag("Player"))
        {
            
            //Set what interactable the player is currently near, try to see if you can get information about what object the player is carrying(keyobject id), save player temporary to be able to kill him?
            other.GetComponent<Controller2D>().setInteractableFocus(this);
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Reset what the interactable the player is currently near, remove what information we got about the player and object the player is carrying(keyobject id)
            var player = other.GetComponent<Controller2D>();
            //Reset what the interactable the player is currently near, remove what information we got about the player and object the player is carrying(keyobject id)
            if (player.GetInteractable() == this)
            {
                
                other.GetComponent<Controller2D>().setInteractableFocus(null);
            }
        }
    }

}

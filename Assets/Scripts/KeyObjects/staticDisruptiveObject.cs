using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
using System;
//[RequireComponent(typeof(Collider2D))]
public abstract class staticDisruptiveObject : MonoBehaviour, IInteractable
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
        if(isOneUse && state != InteractableState.Enabled)
        {
            return false;
        }
        return true;
    }

    public void innitialize(bool isOneUse, InteractableState state)
    {
        this.isOneUse = isOneUse;
        this.state = state;
    }

    public void innitialize()
    {
        isOneUse = false;
        this.state = InteractableState.Enabled;
    }
    public void innitialize(bool isOneUse)
    {
        this.isOneUse = isOneUse;
        this.state = InteractableState.Enabled;
    }
    public void innitialize(InteractableState state)
    {
        this.state = state;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<Controller2D>().setInteractableFocus(this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<Controller2D>().setInteractableFocus(null);
        }
    }
    
}

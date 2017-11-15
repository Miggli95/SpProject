using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
using System;
[RequireComponent(typeof(Collider2D))]
public abstract class staticDisruptiveObject : IInteractable
{

    private bool isOneUse;
    protected InteractableState state;

    public void Enable()
    {
        state = InteractableState.Enabled;
    }


    public virtual bool Interact()
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
    }
    public void innitialize(InteractableState state)
    {
        this.state = state;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;

public abstract class StaticKeyObject : KeyObject, IInteractable
{

    protected string requirement;

    private InteractableState state;
    public abstract void Interact();
    public abstract void flagComplete();

    public StaticKeyObject(string id, string requirement) : base(id)
    {
        this.requirement = requirement;
        state = InteractableState.Dormant;
        
    }

    public StaticKeyObject(string id, string requirement, InteractableState state) : base(id)
    {
        this.requirement = requirement;
        this.state = state;
    }


}

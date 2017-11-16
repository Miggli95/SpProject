using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
public class ko_Pentagram : StaticKeyObject {



    void Start()
    {

        innitialize("Pentagram", new List<string> { "Candle" }, InteractableState.Enabled);

    }

    public new void innitialize(string id, List<string> requirements, InteractableState state)
    {
        base.innitialize(id, requirement, state);
    }

    public override bool Interact(Controller2D player) //going to need a temporary reference to the player to check the item the player is carrying and sacrifice him.
    {
        if (!base.Interact(player))
        {
            return false;
        }
        if(requirement == null || requirement.Count == 0)
        {
            flagComplete();
        }
        
        return true;

    }

    

}

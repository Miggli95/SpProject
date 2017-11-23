using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
public class ko_Dispenser : StaticKeyObject {

    public List<string> items;
    public GameObject potion;

    public void Start()
    {
        base.Initialize("Dispenser", items, InteractableState.Enabled);
    }

    public override bool Interact(Controller2D player)
    {
        if (!base.Interact(player))
        {
            return false;
        }
        if(requirement == null || requirement.Count == 0)
        {
            flagComplete();
            Instantiate(potion, this.transform.position, this.transform.rotation);
            requirement = items;
        }
        else
        {
            interactingPlayer = null;
            state = InteractableState.Enabled;
        }
        return true;
    }

}

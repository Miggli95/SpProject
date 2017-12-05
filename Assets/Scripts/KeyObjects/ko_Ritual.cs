using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
public class ko_Ritual : StaticKeyObject {

    public List<string> potions;
    public string requiredPlayer;
    private Timer timmy;

    public void Start()
    {
        timmy = GameObject.Find("UI Camera").GetComponent<Timer>();
        base.Initialize("Ritual", potions, InteractableState.Enabled);
    }

    public override bool Interact(Controller2D player)
    {
        if (player.name != requiredPlayer)
            return false;
        Debug.Log("Interact called, derived");
        if (!base.Interact(player))
        {
            interactingPlayer = null;
            state = InteractableState.Enabled;
            return false;
        }
        if(requirement == null || requirement.Count == 0)
        {
            flagComplete();
            timmy.doAlch(player.name);
            interactingPlayer.doDeath();

        }
        else
        {
            interactingPlayer = null;
            state = InteractableState.Enabled;
        }
        return true;
    }
}

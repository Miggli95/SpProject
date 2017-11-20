using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
public class do_Door : staticDisruptiveObject {

    public GameObject Door;
    private float timeToOpen = 0.6f;
    private float timer = 0;
    private bool isInteracted;
	
	void Start () {
        innitialize(true);
        Door.GetComponent<Collider>().enabled = false;
	}

    public override bool Interact(Controller2D player)
    {
        if (!base.Interact(player))
        {
            return false;
        }
        if (state == InteractableState.Interacted)
        {
            timer = timeToOpen;
            isInteracted = true;
        }
        if (state == InteractableState.Enabled)
        {
            Close();
        }


        return true;
    }

    void Update()
    {
        if (state != InteractableState.Interacted)
            return;
        if (!isInteracted)
            return;
        if(timer <= 0)
        {
            Open();
        }
        else
        {
            timer -= Time.deltaTime;
        }



           
    }

    private void Open() //call animation event
    {
        //Door.enabled = false;
        Door.GetComponent<Collider>().enabled = false;
        state = InteractableState.Enabled;
        timer = timeToOpen;
        isInteracted = true;
    }

    private void startTimer()
    {
        timer = timeToOpen;
        isInteracted = true;
    }

    private void Close() //call animation event
    {
        //Door.enabled = true;
        Door.GetComponent<Collider>().enabled = true;
        state = InteractableState.Interacted;
    }

}

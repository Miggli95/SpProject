using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
public class do_Door : staticDisruptiveObject {

    public Collider2D triggerZone;
    public Collider2D Door;
    private float timeToOpen = 0.6f;
    private float timer = 0;
    private bool isInteracted;
	
	void Start () {
        innitialize(true);
        Door.enabled = false;
	}

    public override bool Interact()
    {
        if (!base.Interact())
        {
            return false;
        }
        if (state == InteractableState.Enabled)
            Close();
        if (state == InteractableState.Interacted)
        {
            timer = timeToOpen;
            isInteracted = true;
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
        Door.enabled = false;
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
        Door.enabled = true;
        state = InteractableState.Interacted;
    }

}

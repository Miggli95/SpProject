using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
public class do_Door : staticDisruptiveObject {


    
    private float timeToOpen = 0.42f;
    private float timer = 0;
    private bool isInteracted;
    public GameObject Door;
    public Collider DoorCol;
	
	void Start () {
        base.Initialize(true);
        /*var list = GetComponentsInChildren<GameObject>();
        Door = list[1];
        DoorCol = Door.GetComponent<Collider>();*/
        DoorCol.enabled = false;
	}

    public override bool Interact(Controller2D player)
    {
        if (!base.Interact(player))
        {
            return false;
        }
        if (state == InteractableState.Enabled)
        {
            Close();
            return true;
        }
            
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
        Door.transform.Rotate(new Vector3(0, -90));
        DoorCol.enabled = false;
        state = InteractableState.Enabled;
        isInteracted = false;
    }

    private void startTimer()
    {
        timer = timeToOpen;
        isInteracted = true;
    }

    private void Close() //call animation event
    {
        DoorCol.enabled = true;
        Door.transform.Rotate(new Vector3(0, 90));
        state = InteractableState.Interacted;
    }

}

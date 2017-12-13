using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
public class do_Door : staticDisruptiveObject {


    
    private float timeToOpen = 1f;
    private float timer = 0;
    private bool isInteracted;
    public GameObject hinge;
    public GameObject Door;
    public Collider DoorCol;
    private DoorGrace grace;
    private Controller2D player;
    public bool startClosed;
	
	void Start () {
        base.Initialize(true);
        isInteracted = false;
        /*var list = GetComponentsInChildren<GameObject>();
        Door = list[1];
        DoorCol = Door.GetComponent<Collider>();*/
        DoorCol.enabled = false;
        grace = GetComponentInChildren<DoorGrace>();
        //grace = false;
        if (startClosed)
            Close();
	}

    public override bool Interact(Controller2D player)
    {
        if (isInteracted)
            return false;

        if (!base.Interact(player))
        {
            return false;
        }
        this.player = player;
        if (state == InteractableState.Enabled)
        {
            Close();
        } else if (state == InteractableState.Interacted)
        {
            Open();
        }
        startTimer();
        return true;
    }

    void Update()
    {
        if (!isInteracted)
            return;
        if(timer <= 0)
        {
            isInteracted = false;
        }
        else
        {
            timer -= Time.deltaTime;
        }



           
    }

    private void Open() //call animation event
    {
        hinge.transform.Rotate(new Vector3(0, 90));
        //grace.transform.Rotate(new Vector3(0, 90));
        DoorCol.enabled = false;
        state = InteractableState.Enabled;
        isInteracted = false;
    }

    private void startTimer()
    {
        timer = timeToOpen;
    }

    private void Close() //call animation event
    {
        DoorCol.enabled = true;
        hinge.transform.Rotate(new Vector3(0, -90));
        grace.startGrace();
        //grace.transform.Rotate(new Vector3(0, -90));
        state = InteractableState.Interacted;
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        /*if (other.CompareTag("Player"))
        {
            var p = other.GetComponent<Controller2D>();
            if (p == player && grace)
            {
                grace = false;
            }
        }*/
    }

    public void addGrace(Controller2D player) {
        Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), DoorCol, true);
        Physics.IgnoreCollision(player.GetComponent<CharacterController>(), DoorCol, true);

    }

    public void removeGrace(Controller2D player)
    {
        Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), DoorCol, false);
        Physics.IgnoreCollision(player.GetComponent<CharacterController>(), DoorCol, false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interactable;
public class do_Door : staticDisruptiveObject {


    
    private float timeToOpen = 0.15f;
    private float timer = 0;
    private bool isInteracted;
    public GameObject hinge;
    public GameObject Door;
    public Collider DoorCol;
    private DoorGrace grace;
    private Controller2D player;
    public bool startClosed;
    private float rotation;
    private float targetRotation;
    private float currentRotation;
	
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
        /*if(timer <= 0)
        {
            hinge.transform.Rotate(rotation);
            isInteracted = false;
        }
        else
        {
            timer -= Time.deltaTime;
        }*/
        if(currentRotation == targetRotation || currentRotation > 0 || currentRotation < -90)
        {
            Debug.Log(targetRotation + " " + currentRotation);
            hinge.transform.eulerAngles = new Vector3(0, targetRotation * 0.99f);
            currentRotation = targetRotation * 0.99f;
            isInteracted = false;
        }
        else
        {
            hinge.transform.Rotate(new Vector3(0, rotation * Time.deltaTime));
            currentRotation += rotation * Time.deltaTime;
        }




           
    }

    private void Open() //call animation event
    {
        //hinge.transform.Rotate(new Vector3(0, 45));
        //rotation = new Vector3(0, 45f);
        //grace.transform.Rotate(new Vector3(0, 90));
        //hinge.transform.eulerAngles = new Vector3(0, 0f);
        targetRotation += 90;
        rotation = 720f;
        DoorCol.enabled = false;
        state = InteractableState.Enabled;
        isInteracted = true;
        startTimer();
    }

    private void startTimer()
    {
        timer = timeToOpen;
    }

    private void Close() //call animation event
    {
        DoorCol.enabled = true;
        targetRotation += -90;
        rotation = -720f;
        //hinge.transform.eulerAngles = new Vector3(0, -90f);
        //hinge.transform.Rotate(new Vector3(0, -45));
        //rotation = new Vector3(0, -45f);
        grace.startGrace();
        //grace.transform.Rotate(new Vector3(0, -90));
        state = InteractableState.Interacted;
        isInteracted = true;
        startTimer();
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

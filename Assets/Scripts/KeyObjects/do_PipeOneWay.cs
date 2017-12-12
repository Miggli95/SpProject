using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
using interactable;
public class do_PipeOneWay : staticDisruptiveObject {

    public GameObject exitPosition;
    public GameObject Obj;
    public float Timer;
    public float TransferTimePickUp;
    public float TransferTimePlayer;
    private string playerName = null;
    


	// Use this for initialization
	void Start () {
        if (exitPosition == null)
            Debug.LogError("No exit assigned to PipeOneWay");
        base.Initialize(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Obj == null)
            return;

        if (Timer <= 0)
        {
            Obj.transform.position = exitPosition.transform.position;
            if (playerName != null)
            {
                var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
                camera.AddPlayer(playerName);
                Obj.GetComponent<Controller2D>().freeCheckAction();
            }
            Obj = null;
            state = InteractableState.Enabled;
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }

    /*public override bool Interact(Controller2D player)
    {
        if(!base.Interact(player))
            return false;
        state = InteractableState.Interacted;
        var pickUp = player.GetPickUp();
        if(pickUp == null)
        {
            playerName = player.name;
            var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
            camera.RemovePlayer(playerName);
            Obj = player.gameObject;
            Obj.transform.position = new Vector3(0, -50);
            Timer = TransferTimePlayer;
        } else
        {
            player.forceDrop();
            var mb = pickUp as MonoBehaviour;
            Obj = mb.gameObject;
            Obj.transform.position = new Vector3(0, -50);
            Timer = TransferTimePickUp;
        }
        return true;
    }*/

    public override void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Controller2D>();
            if (!base.Interact(player))
                return;
            state = InteractableState.Interacted;
            playerName = player.name;
            player.lockCheckAction();
            var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
            camera.RemovePlayer(playerName);
            Obj = player.gameObject;
            Obj.transform.position = new Vector3(0, -50);
            Timer = TransferTimePlayer;
        }


        
    }

    
}

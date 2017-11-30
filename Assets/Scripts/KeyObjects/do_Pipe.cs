using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
using interactable;
public class do_Pipe : staticDisruptiveObject {

    private Vector3 ExitPosition;
    public do_Pipe Exit;
    public GameObject Obj;
    public float Timer;
    public float TransferTimePickUp;
    public float TransferTimePlayer;
    private string playerName = null;

    public void Start()
    {
        if (Exit == null)
            Debug.LogError("No exit assigned to pipe");
        base.Initialize(false);
        ExitPosition = new Vector3(this.transform.position.x, this.transform.position.y, 0);
    }

    public void Update()
    {
        if (Obj == null)
            return;

        if(Timer <= 0)
        {
            Obj.transform.position = Exit.ExitPosition;
            if(playerName != null)
            {
                var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
                camera.AddPlayer(playerName);
            }
            Obj = null;
            state = InteractableState.Enabled;
            Exit.state = InteractableState.Enabled;
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }
    //TODO change so that players can travel through pipes.
    public override bool Interact(Controller2D player)
    {
        if (!base.Interact(player))
            return false;
        state = InteractableState.Interacted;
        Exit.state = InteractableState.Interacted;
        var pickUp = player.GetPickUp();
        if (pickUp == null)
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
    }

    public Vector3 GetExitPosition()
    {
        return ExitPosition;
    }



}

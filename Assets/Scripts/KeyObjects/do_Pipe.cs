using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public class do_Pipe : staticDisruptiveObject {

    private Vector3 ExitPosition;
    public do_Pipe Exit;
    public IPickUp Obj;
    public float Timer;
    public float TransferTime;

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
            Obj.updatePos(Exit.GetExitPosition());
            Obj = null;
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }
    public override bool Interact(Controller2D player)
    {
        if (!base.Interact(player))
            return false;
        Obj = player.GetPickUp();
        if (Obj == null)
            return false;
        player.forceDrop();
        Obj.updatePos(new Vector3(-100, -100));
        Timer = TransferTime;
        return true;
    }

    public Vector3 GetExitPosition()
    {
        return ExitPosition;
    }



}

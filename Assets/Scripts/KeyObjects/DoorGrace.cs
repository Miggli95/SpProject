using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGrace : MonoBehaviour {

    private do_Door door;
    private bool grace = false;
    private float graceTimer = 0.5f;
    private float timer = 0f;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && grace)
        {
            door.addGrace(other.GetComponent<Controller2D>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.removeGrace(other.GetComponent<Controller2D>());
        }
    }

    public void Start()
    {
        door = GetComponentInParent<do_Door>();
    }

    public void startGrace()
    {
        grace = true;
        timer = 0f;
    }

    public void Update()
    {
        if (!grace)
            return;

        if (timer >= graceTimer)
            grace = false;
        else
        {
            timer += Time.deltaTime;
        }
    }
}

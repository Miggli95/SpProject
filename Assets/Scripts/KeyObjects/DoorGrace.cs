using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGrace : MonoBehaviour {

    private do_Door door;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeishaScript : MonoBehaviour
{
    public float force;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           other.GetComponent<Controller2D>().Boost(force);
            other.GetComponent<Controller2D>().jumping = true;
            SoundManagerScript.PlaySound("boing");
        }
    }
	
}

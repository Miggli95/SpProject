using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetType() == typeof(CharacterController) && !other.GetComponent<Controller2D>().isGhost)
        {
            switch (this.name)
            {
                case "DeathBox":
                    other.GetComponent<Controller2D>().Spawn(new Vector3(5f, 9f, 0));
                    break;
                case "DeathBox (1)":
                    other.GetComponent<Controller2D>().Spawn(new Vector3(-18f, 7f, 0));
                    break;
            }
            //other.transform.position = 
        }
    }
}

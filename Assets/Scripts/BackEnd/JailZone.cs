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
        if(other.GetType() == typeof(CharacterController))
        {
            other.GetComponent<Controller2D>().Spawn(new Vector3(5f, 9f, 0));//other.transform.position = 
        }
    }
}

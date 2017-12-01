using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGrenade : MonoBehaviour {
    private BoxCollider boxy;
    private float timer = 0.1f;
	// Use this for initialization
	void Start () {
        boxy = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
            print(timer);
            timer -=Time.deltaTime;
            if (timer <= 0)
              Destroy(this.gameObject);
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetType()== typeof(CharacterController) && !other.GetComponent<Controller2D>().isGhost)
        {
            other.GetComponent<ControllerKeyManager>().invertControls();
        }
    }
}

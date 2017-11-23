using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : MonoBehaviour {
    private Rigidbody riggy;
    public float maxVelocity = 0.5f;
    private float sqrMaxVelocity;
	// Use this for initialization
	void Start () {
        riggy = this.GetComponent<Rigidbody>();
        setMaxVelocity(maxVelocity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void setMaxVelocity(float max)
    {
        this.maxVelocity = max;
        sqrMaxVelocity = maxVelocity * maxVelocity;
    }
    void fixedUpdate()
    {
        var v = riggy.velocity;
        if(v.sqrMagnitude > sqrMaxVelocity)
        {
            riggy.velocity = v.normalized * maxVelocity;
        }
    }
}

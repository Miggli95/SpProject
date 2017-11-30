using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : MonoBehaviour {
    private Rigidbody riggy;
    public float maxVelocity = 0.5f;
    private float sqrMaxVelocity;
    private GameObject[] potions;
    private GameObject[] players;
    // Use this for initialization
    void Start () {
        riggy = this.GetComponent<Rigidbody>();
        setMaxVelocity(maxVelocity);
        potions = new GameObject[GameObject.FindGameObjectsWithTag("Potion").Length];
        potions = GameObject.FindGameObjectsWithTag("Potion");
        players = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in potions)
        {
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), p.GetComponent<BoxCollider>(), true);
        }


        foreach (GameObject p in players)
        {
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), p.GetComponent<CapsuleCollider>(), true);
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), p.GetComponent<CharacterController>(), true);
        }

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
    public void knockedAway(Vector3 dir)
    {
        riggy.velocity = new Vector3(dir.x, dir.y , 0f);
    }
}

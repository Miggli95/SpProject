using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Projectile projectile;
    public bool targetReached = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setProjectile(Projectile projectile)
    {
        this.projectile = projectile;
    }

    void OnTriggerEnter(Collider other)
    {
        print("other " + other.gameObject.name + "projectile " + projectile.gameObject.name);
        if (other.gameObject.name == projectile.gameObject.name)
        {
            targetReached = true;
            //Spawn Bomb stuff
            Destroy(projectile.gameObject);
            Destroy(gameObject);
        }
    }
}

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

    public void ReachedTarget(GameObject other)
    {
        if (projectile != null)
        {
            print("other " + other.gameObject.name + "projectile " + projectile.gameObject.name);
            if (other.GetComponent<Projectile>() == projectile)
            {
                targetReached = true;
                //Spawn Bomb stuff
                Destroy(projectile.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

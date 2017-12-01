﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float angle;
    float speed;
    public bool shoot = false;
    public Target target;
    Quaternion rotation;
	// Use this for initialization
	void Start () {
		
	}

    public void Shoot(float angle, float speed, Quaternion rotation, Target target)
    {
        this.angle = angle;
        this.speed = speed;
        this.rotation = rotation;
        this.target = target;
        //transform.rotation = rotation;
        shoot = true;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        if (shoot)
        {
            Vector3 dir = Quaternion.AngleAxis(angle, transform.forward) * transform.right;
            transform.Translate(dir * speed * Time.fixedDeltaTime);
            Ray ray = new Ray(transform.position, dir);
            RaycastHit rayhit;
            if (Physics.Raycast(ray, out rayhit, 0.1f))
            {
                if (rayhit.collider.CompareTag("Target"))
                {
                    if (rayhit.collider.gameObject.GetComponent<Target>() == this.target)
                    {
                        target.ReachedTarget(gameObject);
                    }
                }
            }
        }
	}
}

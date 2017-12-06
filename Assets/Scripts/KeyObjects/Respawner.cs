using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour {

    public GameObject spawnerObj;
    public GameObject spawnObj;
    private float timer = 0;
    public float spawnTime;
    private bool spawn = false;

    public void Update()
    {
        if (spawnObj == null)
        {
            print("object gone");
            spawn = true;
        }
        if (spawn && timer >= spawnTime)
        {
            placeObject();
        }
        else if(spawn)
        {
            timer += Time.deltaTime;
        }
    }

    public void placeObject()
    {
        print("trying to spawn new object");
        spawn = false;
        timer = 0;
        Quaternion qart = new Quaternion(0, 0, 0, 0);
        spawnObj=Instantiate(spawnerObj, this.transform.position, qart);
    }
}

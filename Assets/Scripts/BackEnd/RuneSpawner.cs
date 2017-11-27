using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RuneSpawner : MonoBehaviour {
    public float xSpawnRange;
    public float ySpawnRange;
    public float spawnRate = 1.0f;
    public GameObject smallRune;
    private float timeSinceSpawn = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timeSinceSpawn < spawnRate)
        {
            timeSinceSpawn += Time.deltaTime;
        }
        else
        {
            spawn();
            timeSinceSpawn = 0f;
        }
	}
    private void spawn()
    {
        Vector3 randSpawn = new Vector3(this.transform.position.x+Random.Range(-xSpawnRange, xSpawnRange), this.transform.position.y+Random.Range(-ySpawnRange, ySpawnRange), 0f);
        Quaternion randQuant = new Quaternion(0f, 0f, 0f, 0f);
        Instantiate(smallRune, randSpawn, randQuant);
    }
}

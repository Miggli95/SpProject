using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RuneSpawner : MonoBehaviour {
    public float xSpawnRange;
    public float ySpawnRange;
    public float spawnRate = 1.0f;
    public GameObject smallRune;
    private bool okaySpawn = false;
    private float timeSinceSpawn = 0f;
    private Vector3 boxSize = new Vector3(0.25f, 0.25f, 0.25f);
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
        Quaternion randQuant = new Quaternion(0f, 0f, 0f, 0f);
        while (!okaySpawn)
        {
            var randX = Random.Range(-xSpawnRange, xSpawnRange);
            var randY = Random.Range(-ySpawnRange, ySpawnRange);
            Vector3 randSpawn = new Vector3(this.transform.position.x + randX, this.transform.position.y + randY, 0f);
            if (!Physics.CheckBox(randSpawn, boxSize, randQuant))
            {
                Instantiate(smallRune, randSpawn, randQuant);
                okaySpawn = true;
            }

        }
        okaySpawn = false;
    }
}

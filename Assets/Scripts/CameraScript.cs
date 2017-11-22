using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject[] players;
    public float minX, maxX;
    float minY, maxY;
    Camera camera;
    Vector3 target;
    public float originalSize;
    public float currentSize;
    public float time = 0.15f;
    // Use this for initialization
    void Start ()
    {
        minX = float.MaxValue;
        maxX = float.MinValue;
        camera = GetComponent<Camera>();
        target = transform.position;
        originalSize = camera.orthographicSize;
        players = GameObject.FindGameObjectsWithTag("Player");
	}

    // Update is called once per frame
    public bool resetMinX, resetMaxX;
	void Update ()
    {
        resetMinX = true;
        resetMaxX = true;
        float y = 0;
        currentSize = camera.orthographicSize;
        foreach (GameObject p in players)
        {
            Vector2 pos = p.transform.position;
            y += pos.y;

            if (pos.x <= minX)
            {
                print("min " + p.name);
                resetMinX = false;
                minX = pos.x;
            }

            else if (pos.x >= maxX)
            {
                print("max " + p.name);
                resetMaxX = false;
                maxX = pos.x;
            }
        }

        
        float newSize = (maxX - minX) / 2;


        if (newSize > originalSize)
        {
            camera.orthographicSize = newSize;//Mathf.SmoothStep(camera.orthographicSize, newSize, time);
        }

        else
        {
            camera.orthographicSize = originalSize;
        }
        

        target.y = Mathf.SmoothStep(target.y, y / players.Length,time);

        transform.position = target;

        if (resetMinX)
        {
            minX = float.MaxValue;
        }

        if(resetMaxX)
        { 
            maxX = float.MinValue;
           // maxY = float.MinValue;
           // minY = float.MaxValue;
        }
    }
}

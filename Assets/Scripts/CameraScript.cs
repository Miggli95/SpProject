using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject[] players;
    float minX, maxX;
    float minY, maxY;
    Camera camera;
    Vector3 target;
    float originalSize;
    public float minSize = 0;
    public float currentSize;
    public float time = 0.15f;
    // Use this for initialization
    void Start()
    {
        minX = float.MaxValue;
        maxX = float.MinValue;
        camera = GetComponent<Camera>();
        target = transform.position;
        originalSize = camera.orthographicSize;
        if (minSize == 0)
        {
            minSize = originalSize;
        }
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    bool resetMinX, resetMaxX;
    void Update()
    {
        resetMinX = true;
        resetMaxX = true;
        float y = 0;
        float x = 0;
        currentSize = camera.orthographicSize;
        foreach (GameObject p in players)
        {
            Vector2 pos = p.transform.position;
            y += pos.y;
            x += pos.x;
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


        if (newSize > minSize)
        {
            camera.orthographicSize = Mathf.SmoothStep(camera.orthographicSize, newSize, time);
        }

        else
        {
            camera.orthographicSize = Mathf.SmoothStep(camera.orthographicSize, minSize, time);
        }


        target.y = Mathf.SmoothStep(target.y, y / players.Length, time);
        target.x = Mathf.SmoothStep(target.x, x / players.Length, time);
        transform.position = target;

        if (resetMinX)
        {
            minX = float.MaxValue;
        }

        if (resetMaxX)
        {
            maxX = float.MinValue;
            // maxY = float.MinValue;
            // minY = float.MaxValue;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHub : MonoBehaviour
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
    public float offsetSizeX, offsetSizeY = 0;
    public float offsetX, offsetY, offset = 0;
    public bool usingDeltaX = true;
    Vector2 aspectRatio;
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
    bool resetMinX, resetMaxX, resetMinY, resetMaxY;
    void Update()
    {
        resetMinX = true;
        resetMaxX = true;
        resetMinY = true;
        resetMaxY = true;
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
                // print("min " + p.name);
                resetMinX = false;
                minX = pos.x;
            }

            else if (pos.x >= maxX)
            {
                // print("max " + p.name);
                resetMaxX = false;
                maxX = pos.x;
            }

            if (pos.y <= minY)
            {
                // print("min " + p.name);
                resetMinY = false;
                minY = pos.y;
            }

            else if (pos.y >= maxY)
            {
                //  print("max " + p.name);
                resetMaxY = false;
                maxY = pos.y;
            }
        }

        float deltaX = 0, deltaY = 0;
        if (minX != float.MaxValue && maxX != float.MinValue)
            deltaX = Mathf.Abs(((maxX - minX) / camera.aspect) / 2);
        if (minY != float.MaxValue && maxY != float.MinValue)
            deltaY = Mathf.Abs(((maxY - minY) * camera.aspect) / 2);

        //float offsetY = 0;
        float newSize;

        if (deltaX >= deltaY)
        {
            usingDeltaX = true;
            //offsetY = 0;
            newSize = deltaX;
            offset = offsetSizeX;
        }

        else
        {
            usingDeltaX = false;
            // offsetY = 5;
            newSize = deltaY;
            offset = offsetSizeY;
        }


        if (newSize > minSize)
        {
            camera.orthographicSize = Mathf.Abs(Mathf.SmoothStep(camera.orthographicSize, newSize + offset, time));
        }

        else
        {
            camera.orthographicSize = Mathf.Abs(Mathf.SmoothStep(camera.orthographicSize, minSize + offset, time));
        }


        
        // target.y = Mathf.SmoothStep(target.y, (y / players.Length) + offsetY, time);

        target.x = Mathf.SmoothStep(target.x, (x / players.Length) + offsetX, time);
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

        if (resetMinY)
        {
            minY = float.MaxValue;
        }

        if (resetMaxY)
        {
            maxY = float.MinValue;
            // maxY = float.MinValue;
            // minY = float.MaxValue;
        }
    }
}

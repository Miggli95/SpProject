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
    public float time = 0.15f;
    // Use this for initialization
    void Start ()
    {
        camera = GetComponent<Camera>();
        target = transform.position;
        originalSize = camera.orthographicSize;
        players = GameObject.FindGameObjectsWithTag("Player");
	}

    // Update is called once per frame
    bool resetX;
	void Update ()
    {
        resetX = true;
        float y = 0;

        foreach (GameObject p in players)
        {
            Vector2 pos = p.transform.position;
            y += pos.y;

            if (pos.x <= minX)
            {
                resetX = false;
                minX = pos.x;
            }

            else if (pos.x >= maxX)
            {
                resetX = false;
                maxX = pos.x;
            }

            if (pos.y <= minY)
            {
                resetX = false;
                minY = pos.y;
            }

            else if (pos.y >= maxY)
            {
                resetX = false;
                maxY = pos.y;
            }
        }


        float newSize = (maxX - minX) / 2;

        if (!(newSize < originalSize))
        {
            camera.orthographicSize = Mathf.SmoothStep(camera.orthographicSize,newSize,time);
        }

        target.y = Mathf.SmoothStep(target.y, y / players.Length,time);

        transform.position = target;

        if (resetX)
        {
            minX = float.MaxValue;
            maxX = float.MinValue;
            maxY = float.MinValue;
            minY = float.MaxValue;
        }
    }
}

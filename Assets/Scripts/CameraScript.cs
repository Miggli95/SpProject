using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject[] players;
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
    bool updateCamera = false;
    public int size = 0;
    public float maximumSize;
    public bool killCamera;
    public Rect cameraView;
    bool init = false;
    float y = 0;
    float x = 0;
    // Use this for initialization
    void Start()
    {
        if (!init)
        {
            Init();
        }
    }


    public void Init()
    {
        minX = float.MaxValue;
        maxX = float.MinValue;
        camera = GetComponent<Camera>();
        var height = camera.orthographicSize * 2;
        var width = height * camera.aspect;
        cameraView = new Rect(0, 0, width, height);
        cameraView.center = transform.position;
        target = transform.position;
        originalSize = camera.orthographicSize;
        if (minSize == 0)
        {
            minSize = originalSize;
        }
        players = GameObject.FindGameObjectsWithTag("Player");
        size = players.Length;
        init = true;
    }

    public bool Contains(string name,GameObject[] array)
    {
        foreach (GameObject g in array)
        {
            if (g.name == name)
            {
                return true;
            }
        }

        return false;
    }

    public bool RemovePlayer(string name)
    {
        if (Contains(name,players))
        {
            List<GameObject> newList = new List<GameObject>();
            foreach (GameObject g in players)
            {
                if (g.name != name)
                {
                    newList.Add(g);
                }
            }
            players = newList.ToArray();
            size = players.Length;
            updateCamera = true;
            return true;
        }

        return false;
    }

    public bool AddPlayer(string name,string tag = "Player")
    {
        bool success = false;
        GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
        if (Contains(name,array))
        {
            if (!Contains(name, players))
            {
                List<GameObject> newList = new List<GameObject>();
                newList.AddRange(players);
                foreach (GameObject g in array)
                {
                    if (g.name == name)
                    {
                        newList.Add(g);
                        success = true;
                        updateCamera = true;
                        break;
                    }
                }
                players = newList.ToArray();
                size = players.Length;
            }
        }

        return success;
    }

    void KillCamera(float newSize, float offset)
    {
        var height = camera.orthographicSize * 2;
        var width = height * camera.aspect;
        cameraView = new Rect(0, 0, width, height);
        cameraView.center = transform.position;


        foreach (GameObject player in players)
        {
            Vector2 pos = new Vector2(player.transform.position.x, player.transform.position.y);
            if (!cameraView.Contains(pos))
            {
                print("remove player");
                RemovePlayer(player.name);
            }
        }

    }

    // Update is called once per frame
    bool resetMinX, resetMaxX, resetMinY, resetMaxY;
    Vector3 lastTarget;
    void Update()
    {
        resetMinX = true;
        resetMaxX = true;
        resetMinY = true;
        resetMaxY = true;
        lastTarget = target;
        currentSize = camera.orthographicSize;
        foreach (GameObject p in players)
        {
            Vector2 pos = p.transform.position;
            if (pos.x < minX)
            {
               // print("min " + p.name);
                resetMinX = false;
                minX = pos.x;
            }

            if (pos.x > maxX)
            {
               // print("max " + p.name);
                resetMaxX = false;
                maxX = pos.x;
            }

            if (pos.y < minY)
            {
               // print("min " + p.name);
                resetMinY = false;
                minY = pos.y;
            }

            if (pos.y > maxY)
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
       
           
        if (minY != float.MaxValue && maxY != float.MinValue)
            y = (maxY + minY) / 2;
       
        if (minX != float.MaxValue && maxX != float.MinValue)
            x = (maxX + minX) / 2;

        target.y = Mathf.SmoothStep(target.y, y + offsetY, time);

        target.x = Mathf.SmoothStep(target.x, x + offsetX, time);

        if(target!=lastTarget)
        transform.position = target + new Vector3(offsetX, offsetY);


        if (killCamera)
        {
            KillCamera(newSize, offset);
        }

        if (updateCamera)
        {
            resetMinX = true;
            resetMaxX = true;
            resetMinY = true;
            resetMaxY = true;
        }
        if (resetMinX)
        {
            minX = float.MaxValue;
            resetMinX = false;
        }

        if (resetMaxX)
        {
            maxX = float.MinValue;
            resetMaxX = false;
            // maxY = float.MinValue;
            // minY = float.MaxValue;
        }

        if (resetMinY)
        {
            minY = float.MaxValue;
            resetMinY = false;
        }

        if (resetMaxY)
        {
            maxY = float.MinValue;
            resetMaxY = false;
            // maxY = float.MinValue;
            // minY = float.MaxValue;
        }
    }
}

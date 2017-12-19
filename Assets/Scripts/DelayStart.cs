using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayStart : MonoBehaviour {

    public GameObject countDown;
    public static bool gameStarted = false;
	// Use this for initialization
	void Start () {
        StartCoroutine("DelayStart1");
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator DelayStart1()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 5f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        countDown.gameObject.SetActive(false);
        Time.timeScale = 1;
        gameStarted = true;
        // print("working");


    }


    }



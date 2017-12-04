using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayStart : MonoBehaviour {

    public GameObject countDown;
    public GameObject InfoT;

	// Use this for initialization
	void Start () {
        StartCoroutine("DelayStart1");
        InfoT.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator DelayStart1()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        countDown.gameObject.SetActive(false);
        Time.timeScale = 1;
        InfoT.gameObject.SetActive(true);
        // print("working");


    }


    }



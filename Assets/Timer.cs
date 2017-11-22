using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instanceT = null;
    public Text counterText;
    private string levelInstructions = "nlsdsf";
    private float timer = 60f;

    public float seconds, minutes;
    // Use this for initialization
    void Awake()
    {
        if (instanceT == null)
            instanceT = this;

        else if (instanceT != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minutes = (int)(Time.timeSinceLevelLoad / 60f);
        seconds = (int)(Time.timeSinceLevelLoad % 60f);
        counterText.text =levelInstructions + "\n"  + (int)timer;
        timer = timer - Time.deltaTime;
    }
    public void setInstuctions(string s)
    {
        levelInstructions = s;
    }
    public void setTimer(float t)
    {
        timer = t;
    }
    public void playerDied()
    {
        timer = timer / 2;
    }


}
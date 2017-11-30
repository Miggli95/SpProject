using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearMouthZone : MonoBehaviour
{
    public Timer score;

    // Use this for initialization
    void Start()
    {
        score = GameObject.Find("UI Camera").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetType() == typeof(CharacterController))
        {
            CameraScript camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
            camera.RemovePlayer(other.gameObject.name);
            switch (this.name)
            {
                case "30point":
                    score.runeGet(other.name, 30);
                    despawnFirst();
                    other.GetComponent<Controller2D>().doDeath();
                    break;
                case "20point":
                    score.runeGet(other.name, 20);
                    despawnSecond();
                    other.GetComponent<Controller2D>().doDeath();
                    break;
                case "10point":
                    score.runeGet(other.name, 10);
                    despawnThird();
                    other.GetComponent<Controller2D>().doDeath();
                    break;
                     
            }
            
        }
    }
    private void despawnFirst()
    {
        //add more animation/interesting stuff
        GameObject.Find("tempBear").SetActive(false);
        this.gameObject.SetActive(false);
    }
    private void despawnSecond()
    {
        //add more animation/interesting stuff
        this.gameObject.SetActive(false);

    }
    private void despawnThird()
    {
        //add more animation/interesting stuff
        this.gameObject.SetActive(false);
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBearMouth : MonoBehaviour
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

        if (other.GetType() == typeof(CharacterController) && !other.GetComponent<Controller2D>().isGhost)
        {
            // CameraScript camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
            // camera.RemovePlayer(other.gameObject.name);

            if (other.GetComponent<Controller2D>().getAlive())
            {
                switch (this.name)
                {
                    case "30point":
                        score.runeGet(other.name, 30);
                        despawnFirst();
                        other.GetComponent<Controller2D>().doDeath();
                        this.name = "20point";
                        break;
                    case "20point":
                        score.runeGet(other.name, 20);
                        despawnSecond();
                        other.GetComponent<Controller2D>().doDeath();
                        this.name = "10point";
                        break;
                    case "10point":
                        score.runeGet(other.name, 10);
                        despawnThird();
                        other.GetComponent<Controller2D>().doDeath();
                        break;

                }
            }

        }
    }
    private void despawnFirst()
    {
        //add more animation/interesting stuff
        GameObject.Find("BearEvenHungrier").SetActive(false);
        GameObject.Find("BearHungry").SetActive(true);
        this.gameObject.SetActive(false);
    }
    private void despawnSecond()
    {
        //add more animation/interesting stuff

        GameObject.Find("BearHungry").SetActive(false);
        GameObject.Find("Bear").SetActive(true);
        this.gameObject.SetActive(false);

    }
    private void despawnThird()
    {
        //add more animation/interesting stuff
        GameObject.Find("Bear").SetActive(false);
        this.gameObject.SetActive(false);

    }
}

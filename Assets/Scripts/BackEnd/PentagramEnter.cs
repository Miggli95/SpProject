﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramEnter : MonoBehaviour
{
    public GameObject particle;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (other.GetComponent<Controller2D>().getAlive())
        {
            switch (this.transform.parent.name)
            {
                case "PentaRedP":
                    if (other.gameObject.name == "P1")
                    {
                        particle.SetActive(true);
                        SoundManagerScript.PlaySound("ritual");
                    }
                    break;
                case "PentaGreenP":
                    if (other.gameObject.name == "P2")
                    {
                        particle.SetActive(true);
                        SoundManagerScript.PlaySound("ritual");
                    }
                    break;
                case "PentaYellowP":
                    if (other.gameObject.name == "P3")
                    {
                        particle.SetActive(true);
                        SoundManagerScript.PlaySound("ritual");
                    }
                    break;
                case "PentaPurpP":
                    if (other.gameObject.name == "P4")
                    {
                        particle.SetActive(true);
                        SoundManagerScript.PlaySound("ritual");
                    }
                    break;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (other.GetComponent<Controller2D>().getAlive())
        {
            switch (this.transform.parent.name)
            {
                case "PentaRedP":
                    if (other.gameObject.name == "P1")
                    {
                        particle.SetActive(false);
                    }
                    break;
                case "PentaGreenP":
                    if (other.gameObject.name == "P2")
                    {
                        particle.SetActive(false);
                    }
                    break;
                case "PentaYellowP":
                    if (other.gameObject.name == "P3")
                    {
                        particle.SetActive(false);
                    }
                    break;
                case "PentaPurpP":
                    if (other.gameObject.name == "P4")
                    {
                        particle.SetActive(false);
                    }
                    break;

            }
        }
    }
}
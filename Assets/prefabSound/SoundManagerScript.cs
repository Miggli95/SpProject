using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    // Use this for initialization

    public static AudioClip dashSound, dieSound, throwSound, explodeSound, jumpSound;
    static AudioSource audioSrc;
        
	void Start () {

        dashSound = Resources.Load<AudioClip>("dash");

        dieSound = Resources.Load<AudioClip>("die");

        throwSound = Resources.Load<AudioClip>("throw");

        explodeSound = Resources.Load<AudioClip>("explode");

        jumpSound = Resources.Load<AudioClip>("jump");


        //audioSrc = getComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public static void PlaySound (string clip)
    {

        switch(clip)
        {
            case "dash":
                audioSrc.PlayOneShot(dashSound);
                break;
            case "die":
                audioSrc.PlayOneShot(dieSound);
                break;
            case "throw":
                audioSrc.PlayOneShot(throwSound);
                break;
            case "explode":
                audioSrc.PlayOneShot(explodeSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;



        }





    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniPlay : MonoBehaviour {

   // public GameObject BearEvenHungrier2;
    public Animator _animator = null;


    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.tag == "Player")
        {

            _animator.Play("SimpleBearAni");
            
        }
    }

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using UnityEngine;
using System.Collections;

public class AniPlay : MonoBehaviour
{

    public bool enterTrig;
   
    private Animator anim;
  

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
    }


    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            anim.SetBool("SimpleBearAni.anim", true);
            GetComponent<AudioSource>().Play();
            print("works");

        }
    }
}

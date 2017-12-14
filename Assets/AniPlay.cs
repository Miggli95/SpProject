using UnityEngine;
using System.Collections;

public class AniPlay : MonoBehaviour
{

    
   
    public Animator anim;
  

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit(Collider other)
    {
        print("work");
        if (other.CompareTag("Player"))
        {

            anim.Play("SimpleBearAni");

        }


        void OnTriggerEnter(Collider other)
    {
        print("work");
        if (other.CompareTag("Player"))
        {

            anim.Play("SimpleBearAni");


        }


    }
}

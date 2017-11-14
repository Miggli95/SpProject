using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2d_controller : MonoBehaviour
{
    public float speed = 50f;
    public float jumpPower = 150f;
    
    public int jumpCount ;
    public bool grounded;
    private Rigidbody _rigi;

    public Animator anim;
    public AudioClip run_sound;
    public AudioClip jump_sound;
    public AudioClip hurt_sound;

    // Use this for initialization
    void Start()
    {
        _rigi = transform.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // anim.setBool("Grounded", grounded);
      //anim.setBool("Speed",Mathf.Abs(Input.GetAxis("Horizontal")));
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
                //moving the player
        _rigi.AddForce((Vector2.right*speed)*h);

        if (Input.GetKey(KeyCode.Space))
        {
            _rigi.AddForce(Vector3.up * (jumpPower * _rigi.mass * 0.4f));
            jumpCount++;
        }
        // GetComponent<MeshRenderer>().flipX = h.x < 0 ? true : false;
    }
    void OnControllerColliderHit(ControllerColliderHit obj) {
        Debug.Log("we've hit something");
        if (obj.gameObject.tag == "ground")
        {
            Debug.Log("we've hit something");
            grounded = true;
            jumpCount = 0;
        }
    }
    void OnCollisionExit2D(Collision2D obj) {
        if (obj.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }
}
